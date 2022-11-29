using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace SearchAlgo
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Button[] buttons = new System.Windows.Forms.Button[36];
        int[] maze = new int[36];
        int moves = 0;
        ArrayList prev = new ArrayList();
        public Form1()
        {
            InitializeComponent();

            int y = 0;
            int x = 0;
            for (int i = 0; i < 36; i++)
            {
                if (i % 6 == 0 && i != 0)
                {
                    y++;
                    x = 0;
                }
                this.buttons[i] = new System.Windows.Forms.Button();
                this.buttons[i].Location = new System.Drawing.Point(28+x*39, 104+y*29);
                this.buttons[i].BackColor = System.Drawing.SystemColors.Control;
                this.buttons[i].Name = "button"+i+1;
                this.buttons[i].Size = new System.Drawing.Size(40, 30);
                this.buttons[i].TabIndex = 4+i+1;
                int temp = i + 1;
                this.buttons[i].Text = temp+"";
                this.buttons[i].UseVisualStyleBackColor = true;
                this.buttons[i].Click += new System.EventHandler(this.buttons_Click);

                this.Controls.Add(this.buttons[i]);
                x++;
            }

            for(int i = 0; i < 36; i++)
            {
                maze[i] = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            for (int i = 0; i < 36; i++)
            {
                label3.Text = "" + label3.Text + maze[i];

            }*/
            MyQueue q = new MyQueue();
            moves = 0;
            int start = (int)numericUpDown1.Value-1;
            int goal = (int)numericUpDown2.Value-1;

            maze[start] = 1;
            maze[goal] = 2;
        

            int Q = -1, origin = start;
            if (prev.Count != 0) {
                foreach(int i in prev)
                {
                    if(buttons[i].BackColor == System.Drawing.Color.Blue)
                    buttons[i].BackColor = System.Drawing.SystemColors.Control;
                }
                
            }
            try
            {
                if (buttons[start].BackColor == System.Drawing.Color.Red || buttons[goal].BackColor == System.Drawing.Color.Red)
                {
                    throw new Exception("Cannot start at blockage");
                }

                while (origin != goal)
                {
                    if (origin>5 && maze[origin - 6] != -1 && !q.contains(origin - 6))
                    {
                        Q = origin - 6;
                        q.enque(Q, origin);
                    }
                    if (origin<30 && maze[origin + 6] != -1 && !q.contains(origin + 6))
                    {
                        Q = origin + 6;
                        q.enque(Q, origin);
                    }
                    if (origin % 6 != 0 && maze[origin - 1] != -1 && !q.contains(origin - 1))
                    {
                        Q = origin - 1;
                        q.enque(Q, origin);
                    }
                    if ((origin + 1) % 6 != 0 && maze[origin + 1] != -1 && !q.contains(origin + 1))
                    {
                        Q = origin + 1;
                        q.enque(Q, origin);
                    }
                    origin = q.next(origin);
                }

                
                foreach(int n in q.getPath(start, goal))
                {
                    buttons[n].BackColor = System.Drawing.Color.Blue;
                    moves++;
                }
                prev = q.getPath(start, goal);
                String path = "";
                var pathArray = q.getPath(start, goal);
                pathArray.Reverse();
                foreach (int n in pathArray)
                {
                    path += buttons[n].Text + "->";
                }
                string path1 = path.Remove(path.Length-2);
                lblVisit.Text = "Number of visits: " + moves;
                lblBlockage.Text = "Number of blockage: " + countBlockage();
                lblPath.Text = "Path:\n"+path1;
            }
            catch (Exception er)
            {
                label3.Text = "Can't find path, might be trapped";
            }
        }
        private void buttons_Click(object sender, EventArgs e)
        {
            
            Button btn = (Button)sender;

            if (btn.BackColor == System.Drawing.SystemColors.Control) 
            {
                btn.BackColor = System.Drawing.Color.Red;
                btn.UseVisualStyleBackColor = false;
                maze[Int16.Parse(btn.Text) - 1] = -1;
            }     
            else
            {
                btn.BackColor = System.Drawing.SystemColors.Control;
                btn.UseVisualStyleBackColor = true;
                maze[Int16.Parse(btn.Text) - 1] = 1;
            }       
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 36; i++)
            {
                maze[i] = 0;
                buttons[i].BackColor = System.Drawing.SystemColors.Control;

            }
            label3.Text = "Click where to asign obstacle";

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        private int countBlockage() 
        {
            int count = 0;
            for (int i = 0; i < 36; i++)
            {
               if(buttons[i].BackColor == System.Drawing.Color.Red)
               {
                    count++;
               }
            }
            return count;
        }

        private void btnRandomizer_Click(object sender, EventArgs e)
        {
            int num = (int)numericUpDownBlockage.Value;
            var rand = new Random();
            List<int> listNumbers = new List<int>();
            int numbers = rand.Next(0, 36);
            for (int i = 0; i < num; i++)
            {
                do
                {
                    numbers = rand.Next(0, 36);
                } while (listNumbers.Contains(numbers));
                listNumbers.Add(numbers);
            }

            foreach (int i in listNumbers) 
            {
                buttons[i].BackColor = System.Drawing.Color.Red;
                buttons[i].UseVisualStyleBackColor = false;
                maze[Int16.Parse(buttons[i].Text) - 1] = -1;

            }
        }

      
    }
}
