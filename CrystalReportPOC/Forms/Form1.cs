using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess;
using System.Configuration;
using System.Data.OracleClient;
using CrystalReportPOC.Reports;
using CrystalDecisions.CrystalReports.Engine;

namespace CrystalReportPOC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string connStr = ConfigurationManager.ConnectionStrings["YLDBConnStr"].ConnectionString;
            double vatable = 0;
            double taxTotal;
            double grandTotal;

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = "user id=cmsuser;password=UthJurf4#Mekneic7;data source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = oraext9.yleo.us)(PORT = 1521))(CONNECT_DATA=(SERVER = DEDICATED)(SERVICE_NAME = ylext9.yleo.us)))";
            try
            {
                conn.Open();
                OracleCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT o.orderid, o.DateTimeEntered_Utc DateTimeSubmittedIso, o.ShipName ShipToName, o.ShipAddress1 ShipToAddress1, o.ShipAddress2 ShipToAddress2, o.shipcity ShipToCity, o.shipstate ShipToState, o.shipzip ShipToPostal, o.taxamount0 + o.shiptaxtotalamount as VATTax, o.ordertotalamount PackageTotal, o.TaxableTotalAmount + o.shippingchargeamount as VATABLE, c.FedTaxNum CustTIN, c.contactname,  o.membername BusinessName FROM orderqueue_v o, customer c Where o.CustID = c.CustID AND o.orderid = '112803887'";

                OracleDataReader reader = cmd.ExecuteReader();
                rptPHSalesReport rptPHSalesReport = new rptPHSalesReport();
                TextObject txtOrderID = (TextObject)rptPHSalesReport.Section2.ReportObjects["txtOrderID"];
                TextObject txtCustomerName = (TextObject)rptPHSalesReport.Section2.ReportObjects["txtCustomerName"];
                TextObject txtAddress = (TextObject)rptPHSalesReport.Section2.ReportObjects["txtAddress"];
                TextObject txtBusinessName = (TextObject)rptPHSalesReport.Section2.ReportObjects["txtBusinessName"];
                TextObject txtTIN = (TextObject)rptPHSalesReport.Section2.ReportObjects["txtTIN"];
                TextObject txtRecievedDate = (TextObject)rptPHSalesReport.Section2.ReportObjects["txtRecievedDate"];
                TextObject txtDeliveredDate = (TextObject)rptPHSalesReport.Section2.ReportObjects["txtDeliveredDate"];
                TextObject txtTaxTotal = (TextObject)rptPHSalesReport.Section2.ReportObjects["txtTaxTotal"];
                TextObject txtGrandTotal = (TextObject)rptPHSalesReport.Section2.ReportObjects["txtGrandTotal"];



                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtOrderID.Text = reader["ORDERID"].ToString();
                        txtCustomerName.Text = reader["SHIPTONAME"].ToString();
                        txtAddress.Text = reader["SHIPTOADDRESS1"].ToString();
                        txtBusinessName.Text = reader["BUSINESSNAME"].ToString();
                        txtTIN.Text = reader["CUSTTIN"].ToString();
                        txtRecievedDate.Text = reader["DATETIMESUBMITTEDISO"].ToString();
                        txtDeliveredDate.Text = reader["DATETIMESUBMITTEDISO"].ToString();
                        vatable = Convert.ToDouble(reader["VATABLE"]);


                    }

                }
                grandTotal = vatable * 1.2;
                taxTotal = grandTotal - vatable;
                txtTaxTotal.Text = Convert.ToString(taxTotal);
                txtGrandTotal.Text = Convert.ToString(grandTotal);

                salesReportViewer.ReportSource = rptPHSalesReport;
            }
            catch (Exception ex)
            {
                throw (ex);
            }




        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = "user id=cmsuser;password=UthJurf4#Mekneic7;data source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = oraext9.yleo.us)(PORT = 1521))(CONNECT_DATA=(SERVER = DEDICATED)(SERVICE_NAME = ylext9.yleo.us)))";
            try
            {
                conn.Open();

                //to set order details and customer details section 2 and section 
                OracleCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Select c.CustID, c.Name, c.MainAddress1, c.MainAddress2, c.MainAddress3, c.MainCity, c.MainState, c.MainZip" +
                    ",c.RegForGST ,c.gstnumber, c.ABNRegNum, c.CustTypeID, c.email, o.shipweighttotal as Weight" +
                    ",o.datereleasedforshipment, o.datetimeentered, cur.CurrencyID as reportcurrencyid, o.ShipName, o.OrderID, o.SourceID, o.ShippingMethodID" +
                    ",o.WarehouseID, o.ShipAddress1, o.ShipAddress2, o.ShipAddress3, o.ShipCity, o.ShipState, o.ShipZip, o.ShipCountry, o.ShipCountryID, o.ShipPhone" +
                    ",o.CommissionDate, o.DatePackingSlipCreated, o.PriceTotalAmount, o.discountamount, o.pricetypeid, o.TaxRate0, o.taxableamount0" +
                    ",o.gstamount, o.salestaxamount, o.shippingchargeamount, o.ShipRate_Discount_Percent, o.fuelsurchargepercent, o.SHIPPRICETOTALAMOUNT, o.SHIPRATE_AMOUNT_NO_DISCOUNT" +
                    ",o.SalesTotalAmount, o.OTHERPRICE5TOTAL ,o.CODChargeAmount, o.OrderTotalAmount, o.OrderBalance, o.qvtotalamount, o.PSNote, o.Bool01, o.SUPPRESSPRICING, o.csessionid, o.*" +
                    ", w2.Address1 as WarehouseAddress1, w2.Address2 as WarehouseAddress2, w2.City as WarehouseCity, w2.State as WarehouseState" +
                    ", w2.Country as WarehouseCountryName, w2.Zip as WarehousePostalCode, c.gstnumber" +
                    ", nvl(ts.translation, s.name) as sname, nvl(tt.translation, t.name) as tsource" +
                    ", o.InvoiceNumber as SeqInvoiceNumber, o.restockfee, o.restockfee_tax, o.CHARITABLEDONATIONTOTALAMOUNT" +
                    ", decode(o.shipcountry, ' ', 'USA', o.shipcountry) as shipcountryfixed, o.shippingchargeamountstd, o.shippingchargeamountstddisc, o.shippingchargeamountchocolate" +
                    ", decode((select count(oi.itemid) from orditems oi where oi.itemid in (select itemid from itemavailflag where flagid = 31) and " +
                    "oi.OrderID = o.OrderID and o.ShippingMethodID in (60, 61)),0, o.ShippingMethodID, 27) as ShippingMethodIDFixed" +
                    ", decode((select count(oi.itemid) from orditems oi where oi.itemid in (select itemid from itemavailflag where flagid = 31 ) and " +
                    "oi.OrderID = o.OrderID and o.ShippingMethodID in (60, 61)),0, nvl(ts.translation, s.name), 'FedEx IPD Canada') as method, o.PoNumber as PoNumber, o.deliverytime_id,  dt.carriercode as DeliveryTime, o.invoicenumber " +
                    "from customer c, orders o, shipmeth s, trsource t, warehouse w1, warehouse w2, currency cur, pricetyp p, deliverytime dt" +
                    ", (select * from translation where tablename = 'SHIPMETH' and columnname = 'NAME' and languageid = 1) ts" +
                    ", (select * from translation where tablename = 'TRSOURCE' and columnname = 'NAME' and languageid = 1 ) tt " +
                    "where c.custid = o.custid and p.pricetypeid = o.pricetypeid and cur.currencyid = p.currencyid and o.deliverytime_id = dt.id(+) " +
                    "and s.shippingmethodid = ts.pkid(+) and t.sourceid = tt.pkid(+) and o.shippingmethodid = s.shippingmethodid and o.sourceid = t.sourceid " +
                    "and o.warehouseid = w1.warehouseid and nvl(w1.masterwarehouseid, w1.warehouseid) = w2.warehouseid and o.OrderID = '112803887'";

                OracleDataReader reader = cmd.ExecuteReader();
                rptPickSlipUK rptPickSlipUK = new rptPickSlipUK();
                TextObject txtInfo = (TextObject)rptPickSlipUK.Section2.ReportObjects["txtInfo"];
                TextObject txtBillTo = (TextObject)rptPickSlipUK.Section2.ReportObjects["txtBillTo"];
                TextObject txtTotals = (TextObject)rptPickSlipUK.Section2.ReportObjects["txtTotals"];
                TextObject txtTotalsInfo = (TextObject)rptPickSlipUK.Section2.ReportObjects["txtTotalsInfo"];
                TextObject txtOrderTotal = (TextObject)rptPickSlipUK.Section2.ReportObjects["txtOrderTotal"];
                TextObject txtBalance = (TextObject)rptPickSlipUK.Section2.ReportObjects["txtBalance"];
                TextObject txtBillToFooter = (TextObject)rptPickSlipUK.Section2.ReportObjects["txtBillToFooter"];





                double TaxTotal;
                string newLine = Environment.NewLine;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtInfo.Text = Convert.ToString(reader["ORDERID"]) + newLine + Convert.ToString(reader["COMMISSIONDATE"]) + newLine + Convert.ToString(reader["CUSTID"]) + " - "
                                       + Convert.ToString(reader["SHIPNAME"]) + newLine + Convert.ToString(reader["GSTNUMBER"]) + newLine + Convert.ToString(reader["QVTOTALAMOUNT"])
                                       + newLine + Convert.ToString(reader["METHOD"]) + newLine + Convert.ToString(reader["TSOURCE"]);
                        txtBillTo.Text = Convert.ToString(reader["NAME"]) + newLine + Convert.ToString(reader["MAINADDRESS1"]) + newLine + Convert.ToString(reader["MAINADDRESS2"])
                        + newLine + Convert.ToString(reader["MAINADDRESS3"]) + newLine + Convert.ToString(reader["MAINCITY"]) + " " + Convert.ToString(reader["MAINSTATE"]) + " " +
                        Convert.ToString(reader["SHIPZIP"]);
                        txtTotals.Text = "Order Subtotal:" + newLine + "Discount Total:" + newLine + "Package Shipping / Handling:" + newLine + "Tax on Shipping @ " +
                                         reader["TaxRate0"] + "%:" + newLine + "VAT @ " + Convert.ToString(reader["TaxRate0"]) + "%:";

                        if (ToDouble(reader["gstamount"]) == 0 || ToDouble(reader["gstamount"]) == 0) {
                            TaxTotal = Convert.ToDouble(reader["salestaxamount"]);
                                }
                        else {
                            TaxTotal = ToDouble(reader["gstamount"]);
                        }
                        if (ToDouble(reader["salestaxamount"])==0 ) {
                            TaxTotal = ToDouble(reader["gstamount"]);
                                }
                        else {
                            TaxTotal = ToDouble(reader["salestaxamount"]);
                        }

                        txtTotalsInfo.Text = Convert.ToString(reader["PRICETOTALAMOUNT"]) + newLine + Convert.ToString(reader["DISCOUNTAMOUNT"]) +
            newLine + Convert.ToString(reader["SHIPPINGCHARGEAMOUNT"]) + newLine + Convert.ToString(reader["SHIPPRICETOTALAMOUNT"]) + newLine +
               Convert.ToString(TaxTotal);

                        txtOrderTotal.Text = "Order Total:    " + Convert.ToString(reader["ORDERTOTALAMOUNT"]);
                        txtBalance.Text = "Balance:    " + Convert.ToString(reader["ORDERBALANCE"]);
                        txtBillToFooter.Text = Convert.ToString(reader["NAME"]) + newLine + Convert.ToString(reader["MAINADDRESS1"]) + newLine + Convert.ToString(reader["MAINADDRESS2"])
                        + newLine + Convert.ToString(reader["MAINADDRESS3"]) + newLine + Convert.ToString(reader["MAINCITY"]) + " " + Convert.ToString(reader["MAINSTATE"]) + " " +
                        Convert.ToString(reader["SHIPZIP"]) +newLine+ Convert.ToString(reader["SHIPCOUNTRYFIXED"]);

                    }

                }
                //To set values for Payment details section 4
                cmd.CommandText = "Select to_char(trunc(p.datetimeentered), 'dd-Mon-yyyy') As Datetimeentered, p.datetimeentered as PaymentDateTime ," +
                                    " p.paymentmethodid As Paymentmethodid, nvl(t.translation, m.name) As Paymentmethod, o.sourceid As Sourceid, s.Name As MName," +
                                    " Sum(p.debitamount) As Debitamount, Sum(p.creditamount) As Creditamount, nvl(p.currencyid, 1) As Currencyid, p.cardnumbermasked," +
                                    " p.paymentid From Payment p, paymeth m, orders o, trsource s, (Select * From Translation Where upper(tablename) = 'PAYMETH' And " +
                                    "upper(columnname) = 'NAME' And languageid = nvl(1, 1)) t " + "Where p.PaymentMethodID = M.PaymentMethodID And p.OrderID = o.OrderID " +
                                    "And o.Sourceid = s.Sourceid And p.paymentmethodid = t.pkid(+) And p.isauthorized = 'T' And(nvl(p.debitamount, 0) > 0" +
                                    " Or nvl(p.creditamount, 0) > 0) And p.OrderID = '112803887'" +
                                    " Group By to_char(trunc(p.datetimeentered), 'dd-Mon-yyyy'), p.datetimeentered, p.paymentmethodid, nvl(t.translation, m.name), " +
                                    " o.sourceid, s.Name, nvl(p.currencyid, 1),p.cardnumbermasked,p.paymentid Order By Sum(p.debitamount),sum(p.CreditAmount)";
                OracleDataReader reader1 = cmd.ExecuteReader();

                TextObject txtPaymentMethod = (TextObject)rptPickSlipUK.Section2.ReportObjects["txtPaymentMethod"];
                TextObject txtPaymentAmount = (TextObject)rptPickSlipUK.Section2.ReportObjects["txtPaymentAmount"];

                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        txtPaymentMethod.Text = Convert.ToString(reader1["PAYMENTMETHOD"]);
                        txtPaymentAmount.Text = Convert.ToString(reader1["CREDITAMOUNT"]);
                    }
                }

                        salesReportViewer.ReportSource = rptPickSlipUK;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static double ToDouble(object obj, double? defaultValue = 0)
        {
            if (obj == null || obj == "" || obj == DBNull.Value) return 0.0;
            try
            {
                if (obj is string)
                    return double.Parse((string)obj);
                return Convert.ToDouble(obj);
            }
            catch
            {
                if (defaultValue != null) return defaultValue.Value;
                throw;
            }
        }
    }
}
