using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeruletLefedo
{
    class GyepElemKivetel : ApplicationException
    {
        public GyepElemKivetel(IGyepElem gyepElem)
        {
            this._gyepElem = gyepElem;
        }

        IGyepElem _gyepElem;

        public IGyepElem GyepElem
        {
            get
            {
                return _gyepElem;
            }

            set
            {
                _gyepElem = value;
            }
        }
    }

    class GyepElemTulNagyKivetel : GyepElemKivetel
    {
        public GyepElemTulNagyKivetel(IGyepElem gyepElem, ZoldTerulet zoldTerulet) : base(gyepElem)
        {
            this._zoldTerulet = zoldTerulet;
        }

        ZoldTerulet _zoldTerulet;

        public ZoldTerulet ZoldTerulet
        {
            get
            {
                return _zoldTerulet;
            }

            set
            {
                _zoldTerulet = value;
            }
        }
    }
}
