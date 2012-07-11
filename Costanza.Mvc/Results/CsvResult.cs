using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Costanza.Mvc
{
    public class CsvResult : FileResult
    {
        protected readonly DataTable DataTable;
        protected readonly string[] ColumnHeaders;

        public CsvResult( DataTable dataTable )
            : base( "text/csv" )
        {
            this.DataTable = dataTable;
        }

        public CsvResult( DataTable dataTable, string[] columnHeaders )
            : this( dataTable )
        {
            this.ColumnHeaders = columnHeaders;
        }

        protected override void WriteFile( HttpResponseBase response )
        {
            var outputStream = response.OutputStream;
            using( var memoryStream = new MemoryStream() )
            {
                this.WriteDataTable( memoryStream );
                outputStream.Write( memoryStream.GetBuffer(), 0, (int)memoryStream.Length );
            }
        }

        private void WriteDataTable( Stream stream )
        {
            using( var streamWriter = new StreamWriter( stream, Encoding.Default ) )
            {
                this.WriteHeaderLine( streamWriter );
                streamWriter.WriteLine();
                this.WriteDataLines( streamWriter );
            }
        }

        private void WriteHeaderLine( StreamWriter streamWriter )
        {
            if( this.ColumnHeaders != null )
            {
                // Instead of the default column names, write our own custom supplied column names.
                foreach( string name in this.ColumnHeaders )
                {
                    WriteValue( streamWriter, name );
                }
                return;
            }

            foreach( DataColumn dataColumn in this.DataTable.Columns )
            {
                WriteValue( streamWriter, dataColumn.ColumnName );
            }
        }

        private void WriteDataLines( StreamWriter streamWriter )
        {
            foreach( DataRow dataRow in this.DataTable.Rows )
            {
                foreach( DataColumn dataColumn in this.DataTable.Columns )
                {
                    WriteValue( streamWriter, dataRow[ dataColumn.ColumnName ].ToString() );
                }
                streamWriter.WriteLine();
            }
        }

        private static void WriteValue( StreamWriter writer, String value )
        {
            writer.Write( "\"" );
            writer.Write( value.Replace( "\"", "\"\"" ) );
            writer.Write( "\"{0}".FormatWith( CultureInfo.CurrentCulture.TextInfo.ListSeparator ) );
        }
    }
}