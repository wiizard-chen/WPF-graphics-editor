using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace PrintTest
{
    public class TestDocumentPaginator : DocumentPaginator
    {
        public override bool IsPageCountValid
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int PageCount
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override Size PageSize
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override IDocumentPaginatorSource Source
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override DocumentPage GetPage(int pageNumber)
        {
            throw new NotImplementedException();
        }
    }
}
