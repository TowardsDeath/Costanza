using System.Data;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Costanza.Mvc
{
    /// <summary>
    /// Action result that writes a comma separated file to the response.
    /// </summary>
    public class CsvResult : FileResult
    {
        /// <summary>
        /// Data that will be transformed to CSV.
        /// </summary>
        protected readonly DataTable DataTable;

        /// <summary>
        /// Override for the column headers in the data table.
        /// </summary>
        protected readonly string[] ColumnHeaders;

        /// <summary>
        /// Initializes a new instance of the <c>CsvResult</c> class
        /// with the given data table.
        /// </summary>
        /// <param name="dataTable">The data that will be transformed to CSV.</param>
        public CsvResult( DataTable dataTable )
            : base( "text/csv" )
        {
            this.DataTable = dataTable;
        }

        /// <summary>
        /// Initializes a new instance of the <c>CsvResult</c> class
        /// with the given data table and column header overrides.
        /// </summary>
        /// <param name="dataTable">The data that will be transformed to CSV.</param>
        /// <param name="columnHeaders">/// Override for the column headers in the data table.</param>
        public CsvResult( DataTable dataTable, string[] columnHeaders )
            : this( dataTable )
        {
            this.ColumnHeaders = columnHeaders;
        }

        /// <summary>
        /// Implements the WriteFile from the base class by writing
        /// the DataTable to the supplied response in CSV format.
        /// </summary>
        /// <param name="response">The response to write to.</param>
        protected override void WriteFile( HttpResponseBase response )
        {
            this.WriteDataTable( response.OutputStream );
        }

        /// <summary>
        /// Writes the data table to the supplied stream.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        private void WriteDataTable( Stream stream )
        {
            using( var streamWriter = new StreamWriter( stream ) )
            {
                this.WriteHeaderLine( streamWriter );
                streamWriter.WriteLine();
                this.WriteDataLines( streamWriter );
            }
        }

        /// <summary>
        /// Writes a line of header text to identify each data piece in the data table.
        /// </summary>
        /// <param name="streamWriter">Writer that will write the header line to a stream.</param>
        private void WriteHeaderLine( StreamWriter streamWriter )
        {
            if( this.ColumnHeaders != null )
            {
                /* Write the custom supplied column names, instead of the default 
                 * column names that are specified by the DataTable. */
                foreach( string name in this.ColumnHeaders )
                {
                    this.WriteValue( streamWriter, name );
                }
                return;
            }

            /* Write the column names from the data table. */
            foreach( DataColumn column in this.DataTable.Columns )
            {
                this.WriteValue( streamWriter, column.ColumnName );
            }
        }

        /// <summary>
        /// Writes all data from the data table to a stream.
        /// </summary>
        /// <param name="streamWriter">Writer that will write the data to a stream.</param>
        private void WriteDataLines( StreamWriter streamWriter )
        {
            foreach( DataRow row in this.DataTable.Rows )
            {
                foreach( DataColumn column in this.DataTable.Columns )
                {
                    this.WriteValue( streamWriter, row[ column.ColumnName ].ToString() );
                }
                streamWriter.WriteLine();
            }
        }

        /// <summary>
        /// Writes the specified string with the specified writer, escaping
        /// the data according to the CSV format while doing so.
        /// </summary>
        /// <param name="writer">Writer that will write the value to a stream.</param>
        /// <param name="value">The value to write.</param>
        private void WriteValue( StreamWriter writer, string value )
        {
            writer.Write( "\"" );
            writer.Write( value.Replace( "\"", "\"\"" ) );
            writer.Write( "\"{0}".FormatWith( CultureInfo.CurrentCulture.TextInfo.ListSeparator ) );
        }
    }
}