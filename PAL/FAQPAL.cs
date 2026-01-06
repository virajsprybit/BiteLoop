namespace PAL
{
    using System;

    public class FAQPAL
    {
        private string _Answer;
        private string _Date;
        private int _ID;
        private string _Question;
        private int _Status;

        public string Answer
        {
            get
            {
                return this._Answer;
            }
            set
            {
                this._Answer = value;
            }
        }

        public string Date
        {
            get
            {
                return this._Date;
            }
            set
            {
                this._Date = value;
            }
        }

        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this._ID = value;
            }
        }

        public string Question
        {
            get
            {
                return this._Question;
            }
            set
            {
                this._Question = value;
            }
        }

        public int Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                this._Status = value;
            }
        }
    }
}

