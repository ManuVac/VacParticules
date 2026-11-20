Imports System
Imports System.Drawing

Public Class Particule
    Public Property X As Double
    Public Property Y As Double
    Public Property Vx As Double
    Public Property Vy As Double
    Public Property Rayon As Double = 2.5
    Public Property Couleur As Color = Color.FromArgb(120, 200, 255)

    Public Function Vitesse() As Double
        Return Math.Sqrt(Vx * Vx + Vy * Vy)
    End Function
End Class
