Imports System

Public Module ConstantesPhysique
    Public Const kB As Double = 1.380649E-23
    Public Const DiametreMolecule As Double = 3.7E-10
    Public Const MasseN2 As Double = 28.0134 * 1.66053906660E-27
    Public Const MbarEnPa As Double = 100.0

    Public Function LibreParcoursMoyen(pressionMbar As Double, temperatureK As Double) As Double
        Dim pPa As Double = pressionMbar * MbarEnPa
        If pPa <= 0.0 Then Return Double.PositiveInfinity
        Return kB * temperatureK / (Math.Sqrt(2.0) * Math.PI * DiametreMolecule * DiametreMolecule * pPa)
    End Function

    Public Function RegimeVide(pressionMbar As Double) As String
        If pressionMbar >= 1.0 Then Return "pression atmospherique"
        If pressionMbar >= 0.001 Then Return "vide primaire (rough)"
        If pressionMbar >= 1.0E-9 Then Return "haut vide (HV)"
        If pressionMbar >= 1.0E-12 Then Return "ultra haut vide (UHV)"
        Return "extreme haut vide (XHV)"
    End Function

    Public Function VitesseRms(temperatureK As Double) As Double
        Return Math.Sqrt(3.0 * kB * temperatureK / MasseN2)
    End Function

    Public Function FormaterDistance(m As Double) As String
        If Double.IsPositiveInfinity(m) Then Return "infini"
        If m >= 1000.0 Then Return (m / 1000.0).ToString("0.0") & " km"
        If m >= 1.0 Then Return m.ToString("0.00") & " m"
        If m >= 0.001 Then Return (m * 1000.0).ToString("0.0") & " mm"
        If m >= 1.0E-6 Then Return (m * 1.0E6).ToString("0.0") & " um"
        Return (m * 1.0E9).ToString("0.0") & " nm"
    End Function
End Module
