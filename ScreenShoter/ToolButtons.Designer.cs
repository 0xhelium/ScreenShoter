namespace ScreenShoter
{
    partial class ToolButtons
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolButtons));
            this.picClose = new System.Windows.Forms.PictureBox();
            this.picOk = new System.Windows.Forms.PictureBox();
            this.picSave = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSave)).BeginInit();
            this.SuspendLayout();
            // 
            // picClose
            // 
            this.picClose.BackColor = System.Drawing.Color.Transparent;
            this.picClose.Image = ((System.Drawing.Image)(resources.GetObject("picClose.Image")));
            this.picClose.Location = new System.Drawing.Point(370, 3);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(24, 21);
            this.picClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picClose.TabIndex = 3;
            this.picClose.TabStop = false;
            // 
            // picOk
            // 
            this.picOk.BackColor = System.Drawing.Color.Transparent;
            this.picOk.Image = ((System.Drawing.Image)(resources.GetObject("picOk.Image")));
            this.picOk.Location = new System.Drawing.Point(400, 3);
            this.picOk.Name = "picOk";
            this.picOk.Size = new System.Drawing.Size(24, 21);
            this.picOk.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picOk.TabIndex = 3;
            this.picOk.TabStop = false;
            // 
            // picSave
            // 
            this.picSave.BackColor = System.Drawing.Color.Transparent;
            this.picSave.Image = ((System.Drawing.Image)(resources.GetObject("picSave.Image")));
            this.picSave.Location = new System.Drawing.Point(340, 3);
            this.picSave.Name = "picSave";
            this.picSave.Size = new System.Drawing.Size(24, 21);
            this.picSave.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSave.TabIndex = 3;
            this.picSave.TabStop = false;
            // 
            // ToolButtons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.picOk);
            this.Controls.Add(this.picSave);
            this.Controls.Add(this.picClose);
            this.Name = "ToolButtons";
            this.Size = new System.Drawing.Size(432, 28);
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSave)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox picClose;
        private System.Windows.Forms.PictureBox picOk;
        private System.Windows.Forms.PictureBox picSave;
    }
}
