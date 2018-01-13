namespace artemisa
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.ipAddress = new System.Windows.Forms.TextBox();
            this.stop = new System.Windows.Forms.Button();
            this.scan = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.OutPut = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.escanerDeRed = new System.Windows.Forms.RadioButton();
            this.deteccion = new System.Windows.Forms.RadioButton();
            this.deteccionPuertos = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ipAddress
            // 
            this.ipAddress.Location = new System.Drawing.Point(762, 89);
            this.ipAddress.Name = "ipAddress";
            this.ipAddress.Size = new System.Drawing.Size(156, 22);
            this.ipAddress.TabIndex = 0;
            this.ipAddress.TextChanged += new System.EventHandler(this.ipAddress_TextChanged);
            // 
            // stop
            // 
            this.stop.Location = new System.Drawing.Point(843, 198);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(75, 30);
            this.stop.TabIndex = 2;
            this.stop.Text = "stop";
            this.stop.UseVisualStyleBackColor = true;
            this.stop.Click += new System.EventHandler(this.stop_Click);
            // 
            // scan
            // 
            this.scan.Location = new System.Drawing.Point(762, 198);
            this.scan.Name = "scan";
            this.scan.Size = new System.Drawing.Size(75, 30);
            this.scan.TabIndex = 3;
            this.scan.Text = "scan";
            this.scan.UseVisualStyleBackColor = true;
            this.scan.Click += new System.EventHandler(this.scan_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(759, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Estado:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // OutPut
            // 
            this.OutPut.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.OutPut.Location = new System.Drawing.Point(4, 1);
            this.OutPut.Name = "OutPut";
            this.OutPut.Size = new System.Drawing.Size(462, 390);
            this.OutPut.TabIndex = 1;
            this.OutPut.UseCompatibleStateImageBehavior = false;
            this.OutPut.View = System.Windows.Forms.View.Details;
            this.OutPut.SelectedIndexChanged += new System.EventHandler(this.OutPut_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "IP/Puerto";
            this.columnHeader1.Width = 95;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Info";
            this.columnHeader2.Width = 300;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Estatus";
            this.columnHeader3.Width = 64;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(473, 368);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(558, 23);
            this.progressBar1.TabIndex = 6;
            this.progressBar1.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::artemisa.Properties.Resources.artemisa;
            this.pictureBox1.Location = new System.Drawing.Point(473, 89);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(572, 312);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // escanerDeRed
            // 
            this.escanerDeRed.AutoSize = true;
            this.escanerDeRed.Location = new System.Drawing.Point(762, 171);
            this.escanerDeRed.Name = "escanerDeRed";
            this.escanerDeRed.Size = new System.Drawing.Size(131, 21);
            this.escanerDeRed.TabIndex = 8;
            this.escanerDeRed.TabStop = true;
            this.escanerDeRed.Text = "Escaner de Red";
            this.escanerDeRed.UseVisualStyleBackColor = true;
            // 
            // deteccion
            // 
            this.deteccion.AutoSize = true;
            this.deteccion.Location = new System.Drawing.Point(762, 144);
            this.deteccion.Name = "deteccion";
            this.deteccion.Size = new System.Drawing.Size(250, 21);
            this.deteccion.TabIndex = 9;
            this.deteccion.TabStop = true;
            this.deteccion.Text = "Deteccion OS y Usuario (Windows)";
            this.deteccion.UseVisualStyleBackColor = true;
            // 
            // deteccionPuertos
            // 
            this.deteccionPuertos.AutoSize = true;
            this.deteccionPuertos.Location = new System.Drawing.Point(762, 117);
            this.deteccionPuertos.Name = "deteccionPuertos";
            this.deteccionPuertos.Size = new System.Drawing.Size(153, 21);
            this.deteccionPuertos.TabIndex = 10;
            this.deteccionPuertos.TabStop = true;
            this.deteccionPuertos.Text = "Escaner de puertos";
            this.deteccionPuertos.UseVisualStyleBackColor = true;
            this.deteccionPuertos.CheckedChanged += new System.EventHandler(this.deteccionLinux_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1039, 403);
            this.Controls.Add(this.deteccionPuertos);
            this.Controls.Add(this.deteccion);
            this.Controls.Add(this.escanerDeRed);
            this.Controls.Add(this.OutPut);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.stop);
            this.Controls.Add(this.scan);
            this.Controls.Add(this.ipAddress);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "artemisa";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ipAddress;
        private System.Windows.Forms.Button stop;
        private System.Windows.Forms.Button scan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView OutPut;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.RadioButton escanerDeRed;
        private System.Windows.Forms.RadioButton deteccion;
        private System.Windows.Forms.RadioButton deteccionPuertos;
    }
}

