﻿namespace Paway.Windows.Test
{
    partial class CustomControl1
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.directionButton1 = new Paway.Windows.Forms.Metro.DirectionButton();
            this.SuspendLayout();
            // 
            // directionButton1
            // 
            this.directionButton1.Direction = Paway.HX.Enums.EDirection.Up;
            this.directionButton1.Location = new System.Drawing.Point(0, 0);
            this.directionButton1.Name = "directionButton1";
            this.directionButton1.Size = new System.Drawing.Size(80, 20);
            this.directionButton1.TabIndex = 0;
            this.directionButton1.Text = "directionButton1";
            this.ResumeLayout(false);

        }

        #endregion

        private Paway.Windows.Forms.Metro.DirectionButton directionButton1;

    }
}
