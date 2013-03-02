namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.urlEntry = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.directoryEntry = new System.Windows.Forms.TextBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.downloadButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Lien du manga :";
            // 
            // urlEntry
            // 
            this.urlEntry.Location = new System.Drawing.Point(102, 10);
            this.urlEntry.Name = "urlEntry";
            this.urlEntry.Size = new System.Drawing.Size(170, 20);
            this.urlEntry.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Dossier :";
            // 
            // directoryEntry
            // 
            this.directoryEntry.Location = new System.Drawing.Point(102, 48);
            this.directoryEntry.Name = "directoryEntry";
            this.directoryEntry.Size = new System.Drawing.Size(140, 20);
            this.directoryEntry.TabIndex = 3;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(242, 48);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(30, 20);
            this.buttonBrowse.TabIndex = 4;
            this.buttonBrowse.Text = "...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // downloadButton
            // 
            this.downloadButton.Location = new System.Drawing.Point(102, 74);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(96, 23);
            this.downloadButton.TabIndex = 5;
            this.downloadButton.Text = "Téléchargement";
            this.downloadButton.UseVisualStyleBackColor = true;
            this.downloadButton.Click += new System.EventHandler(this.downloadButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 122);
            this.Controls.Add(this.downloadButton);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.directoryEntry);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.urlEntry);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Manga Downloader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox urlEntry;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox directoryEntry;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Button downloadButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}

