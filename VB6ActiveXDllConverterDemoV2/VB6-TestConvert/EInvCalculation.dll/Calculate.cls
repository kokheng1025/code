VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "Calculate"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Implements esl.IExactConnection2

Private m_vParams As New ValueSet 'FCR 20.685.356 - 380 release: Routing in production orders (Rework) - New property

'FC 40.649.406: For E5Shell. Moved global variables to class.
Private mApp                    As esl.Application
Private mComp                   As esl.Company
Private mTerm                   As esl.ITranslator
Private mConn                   As EdlConnection

Public Enum TransactionType
    TransInvoice = 0
    TransSalesOrder = 1
    TransProductionOrder = 2
End Enum

Private Sub Class_Initialize()
    Set mApp = GetApp ' App
    'BR40.344.213 - Sometime call from EStkProduction.dll cannot initialize App, this is to avoid Object variable not declared RTE.
    If Not mApp Is Nothing Then
        Set mComp = mApp.DefaultCompany
        Set mConn = mComp
        Set mTerm = mApp.Term
    End If
End Sub

Private Sub Class_Terminate()
    Set mApp = Nothing
    Set mComp = Nothing
    Set mConn = Nothing
    Set mTerm = Nothing
End Sub

Private Property Let IExactConnection2_Application(ByVal RHS As esl.IApplication)
    Set mApp = RHS
End Property

Private Property Let IExactConnection2_Company(ByVal RHS As esl.ICompany)
    Set mComp = RHS
    Set mConn = mComp
End Property

Private Sub IExactConnection2_Connect()

End Sub

Private Property Let IExactConnection2_Translator(ByVal RHS As esl.ITranslator)
    Set mTerm = RHS
End Property

'*************************************************************************************************
'FCR 20.685.356 - 380 release: Routing in production orders (Rework) - New property
'*************************************************************************************************
Public Property Get Params() As ValueSet
    Set Params = m_vParams
End Property

Public Property Set Params(ByVal vParams As ValueSet)
    Set m_vParams = vParams
End Property

Public Function PhantomAmount(ByVal transType As TransactionType, ByVal sPhantomItem As String, ByRef dSalesAmount As Double, ByVal sKey1 As String, Optional ByVal sKey2 As String) As Double
    Dim sSql As String
    Dim vTotal As Variant
    Dim i As Long
    Dim bEmpty As Boolean
    Dim vData As Variant, vAssortment() As Variant, dPerc As Double, dSalesPrice As Double
    Dim sFaktuurnr As String
    
    ReDim vAssortment(1 To 10)
    
    sSql = "SELECT Class_01, Class_02, Class_03, Class_04, Class_05, Class_06, Class_07, Class_08, Class_09, Class_10, " & _
           "SalesPackagePrice, Percentage_1 FROM items " & _
           "WHERE ItemCode = ?"
           
    vData = mConn.Query(sSql, sPhantomItem, edlNoNulls)
    
    If Not IsEmpty(vData) Then
        vAssortment(1) = vData(1)
        vAssortment(2) = vData(2)
        vAssortment(3) = vData(3)
        vAssortment(4) = vData(4)
        vAssortment(5) = vData(5)
        vAssortment(6) = vData(6)
        vAssortment(7) = vData(7)
        vAssortment(8) = vData(8)
        vAssortment(9) = vData(9)
        vAssortment(10) = vData(10)
        dSalesPrice = CDbl(vData(11))
        dPerc = CDbl(vData(12))
    End If
    
    If dPerc <> 0# Then
        dSalesAmount = CalculateBaseAmount(transType, sKey1, sKey2)
        PhantomAmount = dSalesAmount
        
        For i = LBound(vAssortment) To UBound(vAssortment)
            If Len(Trim(vAssortment(i))) <> 0 Then
                bEmpty = False
                Exit For
            Else
                bEmpty = True
            End If
        Next
            
        If Not bEmpty Then
            Select Case transType
                Case TransSalesOrder
                    '10.062.219, Phantom discount over item,assortments not available anymore.
                    sSql = "SELECT SUM(bdr_ev_ed_val) FROM orsrg JOIN items ON orsrg.artcode = items.itemcode WHERE orsrg.ordernr = ? AND orsrg.ar_soort <> 'P'" & GetItemClass(vAssortment, transType)
                    vTotal = mConn.Query(sSql, sKey1, edlNoNulls)
                
                Case TransInvoice
                    sSql = "SELECT SUM(bdr_ev_ed_val) FROM frsrg JOIN items ON frsrg.artcode = items.itemcode WHERE frsrg.fakt_code = ? AND frsrg.volgnr5 = ? AND frsrg.ar_soort <> 'P'" & GetItemClass(vAssortment)
                    vTotal = mConn.Query(sSql, Array(sKey1, sKey2), edlNoNulls)
                
                Case TransProductionOrder
                    'FC 12.568.654 tanl4384
                    'Exclude by-product items. (by-product => aantal>0 for transsubtype='B' and GLAccountDistribution)
                    'Added 'AND aantal<0'
                    sFaktuurnr = GetFaktuurnr(sKey1)
                    sSql = "SELECT -SUM(bdr_hfl) FROM gbkmut " & _
                        "JOIN Items ON gbkmut.artcode = Items.ItemCode " & _
                        "WHERE Items.Type <> 'P' AND gbkmut.transsubtype = 'B' AND gbkmut.faktuurnr=? " & _
                        "AND gbkmut.reknr = Items.GLAccountDistribution " & _
                        "AND gbkmut.aantal <0 AND " & ProductionSQL & GetItemClassProd(vAssortment, "Items")
                        
                    If Not m_vParams Is Nothing Then
                        If m_vParams.Find("Step", "") Then
                            If Val(m_vParams.Value("Step")) <> 0 Then sSql = sSql & " AND gbkmut.Step = " & m_vParams.Value("Step")
                        End If
                    End If
                        
                    vTotal = mConn.Query(sSql, Array(Format$(Trim$(sFaktuurnr), "@@@@@@@@")), edlNoNulls)
            End Select
            
            If Not IsEmpty(vTotal) Then
                dSalesAmount = vTotal
                PhantomAmount = dSalesAmount
            Else
                dSalesAmount = 0
                PhantomAmount = 0
            End If
        End If
        
        PhantomAmount = PhantomAmount * dPerc / 100
    Else
        If transType <> TransProductionOrder Then
            PhantomAmount = dSalesPrice
            'BR 20.148.132 chon108475: Price = 0 when reapply rules to commission invoice
            dSalesAmount = dSalesPrice
        Else
            PhantomAmount = 0
        End If
    End If
    
End Function

Private Function GetItemClass(vAssortment() As Variant, Optional transType As TransactionType) As String
    Dim i As Long
    Dim sStr As String
    
    '10.062.219, Phantom discount over item,assortments not available anymore
    'optional parameter TransactionType added, to handle the above requirement.
    For i = LBound(vAssortment) To UBound(vAssortment)
        If transType = TransSalesOrder Then
            If Len(Trim(vAssortment(i))) <> 0 Then
                If Len(sStr) = 0 Then sStr = " AND ( " Else sStr = sStr & " OR"
                sStr = sStr & " class_" & Format(i, "00") & " = " & mConn.Literal(vAssortment(i))
            End If
        Else
            If Len(Trim(vAssortment(i))) <> 0 Then
                'BR 12.746.960 Check for assortments also for phantom discount over item
                'sStr = sStr & " AND (class_" & Format(i, "00") & " IS NULL OR class_" & Format(i, "00") & " = " & mConn.Literal(vAssortment(i)) & ")"
                If Len(sStr) = 0 Then sStr = " AND ( " Else sStr = sStr & " OR"
                sStr = sStr & " class_" & Format(i, "00") & " = " & mConn.Literal(vAssortment(i))
            End If
        End If
    Next
    
    If Len(sStr) > 0 Then sStr = sStr & " ) "
    GetItemClass = sStr
    
End Function

'07.040.865
'Only check on items that matches exactly the assortments (no nulls)
Private Function GetItemClassProd(vAssortment() As Variant, ByVal st As String) As String
    Dim i As Long
    Dim sStr As String
    
    For i = LBound(vAssortment) To UBound(vAssortment)
        If Len(Trim(vAssortment(i))) <> 0 Then
            sStr = sStr & " AND (" & st & ".class_" & Format(i, "00") & " = " & mConn.Literal(vAssortment(i)) & ")"
        End If
    Next
    
    GetItemClassProd = sStr
    
End Function

Private Function CalculateBaseAmount(ByVal transType As TransactionType, ByVal sKey1 As String, Optional ByVal sKey2 As String) As Double
    Dim nErr As Long
    Dim sFaktuurnr As String
    Dim ObjLogCalculation As ELogCalculation.CalcFunction

    CalculateBaseAmount = 0
    sFaktuurnr = GetFaktuurnr(sKey1)
    If transType = TransProductionOrder Then
        CalculateBaseAmount = GetTotalProductionAmount(sFaktuurnr, sKey2)
    Else
        Set ObjLogCalculation = New ELogCalculation.CalcFunction
        Call mApp.ConnectObject(ObjLogCalculation, mApp.DefaultCompany, mTerm)
        Select Case transType
            Case TransSalesOrder
                ObjLogCalculation.TablesInfo.intFileType = CALC_FT_ORKRG
                Call ObjLogCalculation.SetParameter(CALC_P_NO_PHANTOM_LINES)
                ObjLogCalculation.TablesInfo.intFileType = CALC_FT_ORKRG
                ObjLogCalculation.TablesInfo.strTableKey1 = sKey1
                ObjLogCalculation.TablesInfo.intLockMode = edlReadOnly
                ObjLogCalculation.DisplayAmountsEx
            Case TransInvoice
                ObjLogCalculation.TablesInfo.intFileType = CALC_FT_FRKRG
                Call ObjLogCalculation.SetParameter(CALC_P_NO_PHANTOM_LINES)
                ObjLogCalculation.TablesInfo.intFileType = CALC_FT_FRKRG
                ObjLogCalculation.TablesInfo.strTableKey1 = sKey1
                ObjLogCalculation.TablesInfo.strTableKey2 = sKey2
                ObjLogCalculation.TablesInfo.intLockMode = edlReadOnly
                ObjLogCalculation.DisplayAmountsEx
        End Select
        CalculateBaseAmount = ObjLogCalculation.DisplayTotal.dblNetAmount
        Set ObjLogCalculation = Nothing
    End If
End Function

Private Function GetTotalProductionAmount(ByVal sProdNumber As String, ByVal sLineNrMainItem As String)
    Dim vInfo As Variant
    Dim sSql As String
    
    'FC 12.568.654 tanl4384
    'Exclude by-product items. (by-product => aantal>0 for transsubtype='B' and GLAccountDistribution)
    'Added 'AND aantal<0'
    sSql = "SELECT -SUM(bdr_hfl) FROM gbkmut " & _
        "JOIN Items ON gbkmut.artcode = Items.ItemCode " & _
        "WHERE Items.Type <> 'P' AND gbkmut.transsubtype = 'B' AND gbkmut.faktuurnr=? " & _
        "AND gbkmut.reknr = Items.GLAccountDistribution AND gbkmut.aantal<0 AND " & ProductionSQL
        
    'FCR 20.685.356: B380 (routing): Filter production cost by step
    If Not m_vParams Is Nothing Then
        If m_vParams.Find("Step", "") Then
            If Val(m_vParams.Value("Step")) <> 0 Then sSql = sSql & " AND gbkmut.Step = " & m_vParams.Value("Step")
        End If
    End If
        
    vInfo = mConn.Query(sSql, Array(Format$(Trim$(sProdNumber), "@@@@@@@@")), edlNoNulls)
    If Not IsEmpty(vInfo) Then
        GetTotalProductionAmount = vInfo
    End If
End Function

Private Function ProductionSQL() As String
     ProductionSQL = " (gbkmut.Transtype = 'B' AND gbkmut.freefield1 = 'P' AND gbkmut.bud_vers='MRP') "
End Function

Private Function GetFaktuurnr(ByVal sID As String) As String
    Dim vInfo As Variant
    
    vInfo = mConn.Query("SELECT faktuurnr FROM gbkmut WHERE ID=?", CLng(sID), edlNoNulls)
    If Not IsEmpty(vInfo) Then
        GetFaktuurnr = vInfo
    End If
End Function
'br:11.936.595 - when distribute de extra/discount charges, shld take assortment into consideration as well
Public Function PhantomAmountGroupByVAT(ByVal transType As TransactionType, ByVal sPhantomItem As String, ByVal sKey1 As String, Optional ByVal sKey2 As String) As EdlQuery
    Dim sSql As String
    Dim edlQ As EdlQuery
    Dim vTotal As Variant
    Dim i As Long
    Dim bEmpty As Boolean
    Dim vData As Variant, vAssortment() As Variant, dPerc As Double
    
    ReDim vAssortment(1 To 10)
    
    sSql = "SELECT Class_01, Class_02, Class_03, Class_04, Class_05, Class_06, Class_07, Class_08, Class_09, Class_10, " & _
           "Percentage_1 FROM items " & _
           "WHERE ItemCode = ?"
           
    vData = mConn.Query(sSql, sPhantomItem, edlNoNulls)
    
    If Not IsEmpty(vData) Then
        vAssortment(1) = vData(1)
        vAssortment(2) = vData(2)
        vAssortment(3) = vData(3)
        vAssortment(4) = vData(4)
        vAssortment(5) = vData(5)
        vAssortment(6) = vData(6)
        vAssortment(7) = vData(7)
        vAssortment(8) = vData(8)
        vAssortment(9) = vData(9)
        vAssortment(10) = vData(10)
        dPerc = (CDbl(vData(11)) / 100)
    End If
    
    If dPerc <> 0# Then
        'br:15.007.244 - degil and stupid -No question prompted to user to split tax evenly
        'no assortment also can continue
        'For i = LBound(vAssortment) To UBound(vAssortment)
        '    If Len(Trim(vAssortment(i))) <> 0 Then
        '        bEmpty = False
        '        Exit For
        '    Else
        '        bEmpty = True
        '    End If
        'Next
            
        'If Not bEmpty Then
            Select Case transType
                Case TransSalesOrder
                    '10.062.219, Phantom discount over item,assortments not available anymore.
                    sSql = "SELECT btw_code,SUM(bdr_ev_ed_val) AS TaxAmount, (SUM(bdr_ev_ed_val)*?) AS TaxDisc FROM orsrg " & _
                            "JOIN items ON orsrg.artcode = items.itemcode " & _
                            "WHERE orsrg.ordernr = ? AND orsrg.ar_soort <> 'P' " & _
                            GetItemClass(vAssortment, transType) & _
                            " GROUP BY btw_code "
                    Set edlQ = mConn.OpenParamQuery(sSql, "orsrg.korting,orsrg.ordernr", edlClientSnapshot, edlReadOnly, edlOptDontParse)
                    Call edlQ.Requery2(dPerc, sKey1)
                Case TransInvoice
                    sSql = "SELECT btw_code, SUM(bdr_ev_ed_val) AS TaxAmount, (SUM(bdr_ev_ed_val)*?) AS TaxDisc FROM frsrg " & _
                            "JOIN items ON frsrg.artcode = items.itemcode " & _
                            "WHERE frsrg.fakt_code = ? AND frsrg.volgnr5 = ? AND frsrg.ar_soort <> 'P' " & _
                            GetItemClass(vAssortment) & _
                            " GROUP BY btw_code "
                    Set edlQ = mConn.OpenParamQuery(sSql, "frsrg.korting,frsrg.fakt_code,frsrg.volgnr5", edlClientSnapshot, edlReadOnly, edlOptDontParse)
                    Call edlQ.Requery3(dPerc, sKey1, sKey2)
            End Select
            Set PhantomAmountGroupByVAT = edlQ
        'End If
    End If

End Function

