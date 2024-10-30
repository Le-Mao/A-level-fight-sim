<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GUI
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Log = New System.Windows.Forms.RichTextBox()
        Me.btnAttack = New System.Windows.Forms.Button()
        Me.btnPower = New System.Windows.Forms.Button()
        Me.PowerSelectorBox = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'Log
        '
        Me.Log.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Log.Location = New System.Drawing.Point(222, 12)
        Me.Log.Name = "Log"
        Me.Log.Size = New System.Drawing.Size(250, 295)
        Me.Log.TabIndex = 0
        Me.Log.Text = ""
        '
        'btnAttack
        '
        Me.btnAttack.Location = New System.Drawing.Point(12, 12)
        Me.btnAttack.Name = "btnAttack"
        Me.btnAttack.Size = New System.Drawing.Size(204, 34)
        Me.btnAttack.TabIndex = 1
        Me.btnAttack.Text = "Attack"
        Me.btnAttack.UseVisualStyleBackColor = True
        '
        'btnPower
        '
        Me.btnPower.Location = New System.Drawing.Point(168, 52)
        Me.btnPower.Name = "btnPower"
        Me.btnPower.Size = New System.Drawing.Size(48, 34)
        Me.btnPower.TabIndex = 2
        Me.btnPower.Text = "Use"
        Me.btnPower.UseVisualStyleBackColor = True
        '
        'PowerSelectorBox
        '
        Me.PowerSelectorBox.FormattingEnabled = True
        Me.PowerSelectorBox.Location = New System.Drawing.Point(12, 60)
        Me.PowerSelectorBox.Name = "PowerSelectorBox"
        Me.PowerSelectorBox.Size = New System.Drawing.Size(150, 21)
        Me.PowerSelectorBox.TabIndex = 3
        '
        'GUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(484, 361)
        Me.Controls.Add(Me.PowerSelectorBox)
        Me.Controls.Add(Me.btnPower)
        Me.Controls.Add(Me.btnAttack)
        Me.Controls.Add(Me.Log)
        Me.Name = "GUI"
        Me.Text = "GUI"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Log As System.Windows.Forms.RichTextBox
    Friend WithEvents btnAttack As System.Windows.Forms.Button
    Friend WithEvents btnPower As System.Windows.Forms.Button
    Friend WithEvents PowerSelectorBox As System.Windows.Forms.ComboBox

End Class
