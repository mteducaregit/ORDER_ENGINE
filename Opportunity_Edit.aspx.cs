﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
//using System.Data.SqlClient.SqlDataReader;
//using Exportxls.BL;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using ShoppingCart.BL;
using System.Text;

public partial class Opportunity_Edit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Request["Opportunity_Code"] != null)
            {
                string oppid = Request["Opportunity_Code"];
                string Menuid = "103";
                HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                string UserID = cookie.Values["UserID"];
                string UserName = cookie.Values["UserName"];
                lblpagetitle1.Text = "Edit Opportunity";
                lblpagetitle2.Text = "";
                //limidbreadcrumb.Visible = true;
                lblmidbreadcrumb.Text = "Manage Opportunity";
                //lilastbreadcrumb.Visible = true;
                lbllastbreadcrumb.Text = "Edit Opportunity";
                //lilastbreadcrumb.Visible = true;
                divSuccessmessage.Visible = false;
                divErrormessage.Visible = false;
                UpnlAdd.Visible = true;
                UpnlSecContact.Visible = false;
                SqlDataReader dr = UserController.Getuserrights(UserID, Menuid);
                try
                {
                    if ((((dr) != null)))
                    {
                        if (dr.Read())
                        {
                            int allowdisplay =Convert.ToInt32(dr["Allow_Add"].ToString());
                            if (allowdisplay == 1)
                            {
                                btnaddOpp.Visible = true;
                                //btnadd2.Visible = True
                                //btnimportOpp.Visible = True
                            }
                            else
                            {
                                btnaddOpp.Visible = false;
                                //btnadd2.Visible = False
                                //btnimportOpp.Visible = False
                            }

                        }
                    }


                }
                catch (Exception ex)
                {
                }

                
                lblusercompany.Text = "MT";
                tdapplicationno.Visible = true;
                tdapplicationno1.Visible = true;

                txt1.Text = DateTime.Now.ToString("dd-MM-yyyy");
                txtdiscount.Text = "0";
                tblorgassign.Visible = false;
                
                ContactSource();
                StudentType();
                //Scorerange();
                Institutetype();
               
                Yearofpassing();
                DivisionSession();
                ContactType();
                BindProductCategory();
                BindSalesStage();
                BindSourceCompany();
                BindTargetCompany();
                BindSalesChannel();
                ContactType3();
                Country2();
              
                this.ddlcurrentstudying2.Items.Insert(0, "Select");
                this.ddlcurrentstudying2.SelectedIndex = 0;
                

                Bindlist();
                //BindSecContact();
                divErrormessage.Visible = false;
                divSuccessmessage.Visible = false;
            }
            else
            {
                Response.Redirect("login.aspx");
            }
        }

    }

    private void ContactSource()
    {
        DataSet ds = ProductController.GetallactiveleadSource();
        BindDDL(ddlContactsourceadd, ds, "Description", "ID");
        ddlContactsourceadd.Items.Insert(0, "Select");
        ddlContactsourceadd.SelectedIndex = 0;
    }

    private void Bindlist()
    {

        try
        {

            if (Request["Opportunity_Code"] != null)
            {
                string opp_id = Request["Opportunity_Code"];
                string Hiphen = "-";
                SqlDataReader dr = ProductController.GetOppdetailsbyoppid(opp_id);
                if ((((dr) != null)))
                {
                    if (dr.Read())
                    {
                        txt1.Text = dr["Created_on"].ToString();
                        ContactType();
                        //ddlcontacttype1.SelectedValue = dr["Con_type_id"].ToString();
                        BindSalesChannel();
                        ddlsaleschannel.SelectedValue = dr["SalesStage_Id"].ToString();

                        if (dr["Con_Title"].ToString() == "Mr.")
                        {
                            //this.ddltitle.SelectedValue = "1";
                            ////Me.ddlgenderadd.SelectedValue = "1"
                        }
                        else
                        {
                            //this.ddltitle.SelectedValue = "2";
                            ////Me.ddlgenderadd.SelectedValue = "2"
                        }

                        
                        lblstudentname.Text = Hiphen + " " + dr["Con_Title"].ToString() + " " + dr["Con_FirstName"].ToString() + " " + dr["Con_midname"].ToString() + " " + dr["Con_lastname"].ToString();
                        
                        lblprimarycontactid.Text = dr["con_id"].ToString();
                        lblConId.Text = dr["Con_Id"].ToString();
                        
                        txtdiscountnotes.Text = dr["disc_remark"].ToString();
                        
                        StudentType();
                        ddlstudenttypeadd.SelectedValue = dr["Category_Type_id"].ToString();

                        if (ddlstudenttypeadd.SelectedValue == "01")
                        {
                            tblorgassign.Visible = true;
                            tblrow1.Visible = true;
                            trSourcecompany.Visible = true;
                            //tdstudentid.Visible = true;
                            //tdstudentid1.Visible = true;
                            //tdlastcourse.Visible = true;
                            //tdlastcourse1.Visible = true;
                            BindSourceCompany();
                            ddlsourcecompanyadd.SelectedValue = dr["Opp_Contact_Company"].ToString();
                            BindSourceDivisionAdd();
                            ddlSourcedivisionadd.SelectedValue = dr["Opp_ContactSource_Division"].ToString();
                            BindSourceZoneAdd();
                            ddlSourcezoneadd.SelectedValue = dr["Opp_ContactSource_Zone"].ToString();
                            BindSourceCenterAdd();
                            ddlSourcecenteradd.SelectedValue = dr["Opp_ContactSource_Center"].ToString();
                            BindTargetCompany();
                            ddltargetcompanyadd.SelectedValue = dr["Opp_Contact_Target_Company"].ToString();
                            BindTargetDivisionadd();
                            ddltargetdivisionadd.SelectedValue = dr["Opp_Contact_Division"].ToString();
                            BindTargetzoneadd();
                            ddltargetzoneadd.SelectedValue = dr["Opp_Contact_Zone"].ToString();
                            BindTargetCenterAdd();
                            ddltargetcenteradd1.SelectedValue = dr["Opp_Contact_Center"].ToString();
                        }
                        else if (ddlstudenttypeadd.SelectedValue == "02")
                        {
                            tblorgassign.Visible = true;
                            tblrow1.Visible = false;
                            trSourcecompany.Visible = false;
                            //tdstudentid.Visible = false;
                            //tdstudentid1.Visible = false;
                            //tdlastcourse.Visible = false;
                            //tdlastcourse1.Visible = false;
                            BindTargetCompany();
                            ddltargetcompanyadd.SelectedValue = dr["Opp_Contact_Target_Company"].ToString();
                            BindTargetDivisionadd();
                            ddltargetdivisionadd.SelectedValue = dr["Opp_Contact_Division"].ToString();
                            BindTargetzoneadd();
                            ddltargetzoneadd.SelectedValue = dr["Opp_Contact_Zone"].ToString();
                            BindTargetCenterAdd();
                            ddltargetcenteradd1.SelectedValue = dr["Opp_Contact_Center"].ToString();

                        }
                        else if (ddlstudenttypeadd.SelectedValue == "03")
                        {
                            tblorgassign.Visible = true;
                            tblrow1.Visible = true;
                            trSourcecompany.Visible = true;
                            //tdstudentid.Visible = true;
                            //tdstudentid1.Visible = true;
                            //tdlastcourse.Visible = true;
                            //tdlastcourse1.Visible = true;
                            BindSourceCompany();
                            ddlsourcecompanyadd.SelectedValue = dr["Opp_Contact_Company"].ToString();
                            BindSourceDivisionAdd();
                            ddlSourcedivisionadd.SelectedValue = dr["Opp_ContactSource_Division"].ToString();
                            BindSourceZoneAdd();
                            ddlSourcezoneadd.SelectedValue = dr["Opp_ContactSource_Zone"].ToString();
                            BindSourceCenterAdd();
                            ddlSourcecenteradd.SelectedValue = dr["Opp_ContactSource_Center"].ToString();
                            BindTargetCompany();
                            ddltargetcompanyadd.SelectedValue = dr["Opp_Contact_Target_Company"].ToString();
                            BindTargetDivisionadd();
                            ddltargetdivisionadd.SelectedValue = dr["Opp_Contact_Division"].ToString();
                            BindTargetzoneadd();
                            ddltargetzoneadd.SelectedValue = dr["Opp_Contact_Zone"].ToString();
                            BindTargetCenterAdd();
                            ddltargetcenteradd1.SelectedValue = dr["Opp_Contact_Center"].ToString();

                        }
                        else if (ddlstudenttypeadd.SelectedValue == "04")
                        {
                            tblorgassign.Visible = true;
                            tblrow1.Visible = false;
                            trSourcecompany.Visible = false;
                            //tdstudentid.Visible = false;
                            //tdstudentid1.Visible = false;
                            //tdlastcourse.Visible = false;
                            //tdlastcourse1.Visible = false;
                            BindTargetCompany();
                            ddltargetcompanyadd.SelectedValue = dr["Opp_Contact_Target_Company"].ToString();
                            BindTargetDivisionadd();
                            ddltargetdivisionadd.SelectedValue = dr["Opp_Contact_Division"].ToString();
                            BindTargetzoneadd();
                            ddltargetzoneadd.SelectedValue = dr["Opp_Contact_Zone"].ToString();
                            BindTargetCenterAdd();
                            ddltargetcenteradd1.SelectedValue = dr["Opp_Contact_Center"].ToString();
                        }
                        else if (ddlstudenttypeadd.SelectedValue == "05") //-- added for online admission-23-06-2020
                        {
                            tblorgassign.Visible = true;
                            tblrow1.Visible = false;
                            trSourcecompany.Visible = false;
                            //tdstudentid.Visible = false;
                            //tdstudentid1.Visible = false;
                            //tdlastcourse.Visible = false;
                            //tdlastcourse1.Visible = false;
                            BindTargetCompany();
                            ddltargetcompanyadd.SelectedValue = dr["Opp_Contact_Target_Company"].ToString();
                            BindTargetDivisionadd();
                            ddltargetdivisionadd.SelectedValue = dr["Opp_Contact_Division"].ToString();
                            BindTargetzoneadd();
                            ddltargetzoneadd.SelectedValue = dr["Opp_Contact_Zone"].ToString();
                            BindTargetCenterAdd();
                            ddltargetcenteradd1.SelectedValue = dr["Opp_Contact_Center"].ToString();
                            ddltargetzoneadd.Enabled = true;
                            ddltargetcenteradd1.Enabled = true;
                            ddlacademicyear.Enabled = true;
                            ddlproduct.Enabled = true;
                        }
                        else
                        {
                            tblorgassign.Visible = false;
                            tblrow1.Visible = true;
                            //tdstudentid.Visible = false;
                            //tdstudentid1.Visible = false;
                            //tdlastcourse.Visible = false;
                            //tdlastcourse1.Visible = false;
                        }

                        if (dr["Is_Branch_Topper"].ToString() == "1")
                        {
                            ckhBranchTopper.Checked = true;

                        }
                        else
                        {
                            ckhBranchTopper.Checked = false;
                        }

                        if (ckhBranchTopper.Checked)
                        {
                            trBranchTopper.Visible = true;
                        }
                        else
                        {
                            trBranchTopper.Visible = false;
                        }
                        if (dr["Is_School_Ranker"].ToString() == "1")
                        {

                            chkSchoolRanker.Checked = true;
                        }
                        else
                        {
                            chkSchoolRanker.Checked = false;
                        }

                        if (chkSchoolRanker.Checked)
                        {
                            trSchoolRanker.Visible = true;
                        }
                        else
                        {
                            trSchoolRanker.Visible = false;
                        }


                        if (dr["Apply_Additional_Discount"].ToString() == "1")
                        {
                            ckhRankerAdditional.Checked = true;

                        }
                        else
                        {
                            ckhRankerAdditional.Checked = false;
                        }


                        if (ckhRankerAdditional.Checked)
                        {
                            trDiscount.Visible = true;
                        }
                        else
                        {
                            trDiscount.Visible = false;
                        }

                        BindBranchTopperDivision();
                        ddlbranchtopperdivision.SelectedValue = dr["Branch_Topper_Division"].ToString();
                        BindBranchTopperCenter();
                        ddlbranchtopperCenter.SelectedValue = dr["Branch_Topper_Center"].ToString();
                        txtschooldivision.Text = dr["School_Division"].ToString();
                        txtschoolrank.Text = dr["School_Rank"].ToString();
                        BindddlInstitute();
                        ddlschoolranker.SelectedValue = dr["School_Name"].ToString();

                        BindDiscountconditions();
                        ddldiscountconditions.SelectedValue = dr["Discount_Type"].ToString();


                        if (lblusercompany.Text == "MPUC")
                        {
                        }
                        else
                        {
                            txtstudentid.Text = dr["Student_Id"].ToString();
                          
                        }

                        BindProductCategory();
                        this.ddlAddProductCategory.SelectedValue = dr["Product_code"].ToString();
                        BindSalesStage();
                        this.ddlAddSalesStage.SelectedValue = dr["Sales_Stage"].ToString();
                        if (ddlAddSalesStage.SelectedValue == "04")
                        {
                            this.txtapplicationno.Enabled = true;
                            this.txtapplicationno.Text = dr["app_no"].ToString();
                        }
                        else
                        {
                            this.txtapplicationno.Enabled = false;
                            this.txtapplicationno.Text = dr["app_no"].ToString();
                        }
                        this.txtprobabilitypercent.Text = dr["opp_probability_percent"].ToString();
                        this.txtjoindate.Text = dr["opp_join_date"].ToString();
                        this.txtexpectedclosedate.Text = dr["opp_expected_close_date"].ToString();
                        BindAcademicYear();
                        this.ddlacademicyear.SelectedValue = dr["Current_Year_Desc"].ToString();
                        BindStream();
                        this.ddlproduct.SelectedValue = dr["oppor_product_id"].ToString();
                        this.txtdiscount.Text = dr["opp_disc"].ToString();
                        this.txtdiscountnotes.Text = dr["disc_remark"].ToString();
                        txtassignedto.Text = dr["opp_contact_Assignto"].ToString();
                        txtassignedto.Enabled = false;
                        //txtdateofbirth.Text = dr["dob"].ToString();
                        //txtexaminationdetails.Text = dr["exam_details"].ToString();

                        BindSecContactDetails2(lblConId.Text);

                        DataSet ds = ProductController.Get_ContactbyContactId(10, opp_id);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataList dlConHistory = (DataList)HistoryPanel1.FindControl("dlConHistory");
                            DataList dlfeedbackhistory = (DataList)HistoryPanel1.FindControl("dlfeedbackhistory");
                            DataList dlCallhistory = (DataList)HistoryPanel1.FindControl("dlCallhistory");

                            if (ds.Tables[3].Rows.Count > 0)
                            {

                                dlConHistory.Visible = true;
                                //lblCon_History.Visible = false;
                                // diverrormessageContactHistory.Visible = false;
                                HistoryPanel1.DivErrorMessageContactHistoryVisibility(false);

                                dlConHistory.DataSource = ds.Tables[3];
                                dlConHistory.DataBind();
                            }
                            else
                            {
                                dlConHistory.Visible = false;
                                HistoryPanel1.DivErrorMessageContactHistoryVisibility(true);
                            }

                            if (ds.Tables[4].Rows.Count > 0)
                            {
                                dlfeedbackhistory.Visible = true;
                                //diverrormessagefeedback.Visible = false;
                                HistoryPanel1.DivErrorMessageFollowupHistoryVisibility(false);
                                dlfeedbackhistory.DataSource = ds.Tables[4];
                                dlfeedbackhistory.DataBind();

                            }
                            else
                            {
                                // divfeedbackhistory.Visible = false;
                                dlfeedbackhistory.Visible = false;
                                HistoryPanel1.DivErrorMessageFollowupHistoryVisibility(true);
                                //diverrormessagefeedback.Visible = true;
                                //lblerrrormessagefeedback.Text = "No Follow up history found !!!";
                            }

                            if (ds.Tables[8].Rows.Count > 0)
                            {
                                dlCallhistory.Visible = true;
                                HistoryPanel1.DivErrorMessageCallHistoryVisibility(false);
                                //diverrormessageCallHistory.Visible = false;
                                dlCallhistory.DataSource = ds.Tables[8];
                                dlCallhistory.DataBind();
                            }
                            else
                            {
                                dlCallhistory.Visible = false;
                                HistoryPanel1.DivErrorMessageCallHistoryVisibility(true);
                                //diverrormessageCallHistory.Visible = true;
                                //lblerrrormessageCallHistory.Visible = true;
                                //lblerrrormessageCallHistory.Text = "No records found..!";
                            }

                        }
                    }
                }
            }
            divErrormessage.Visible = false;
        }
        catch (Exception ex)
        {
            divErrormessage.Visible = true;
            lblerrormessage.Visible = true;
            lblerrormessage.Text = ex.Message;
        }
    }


    protected void btnEditCon_Click(object sender, EventArgs e)
    {
        string url = "Contact_Edit.aspx?&Con_id=" + lblConId.Text;
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.open('");
        sb.Append(url);
        sb.Append("');");
        sb.Append("</script>");
        ClientScript.RegisterStartupScript(this.GetType(),
                "script", sb.ToString());
    }

    protected void btnRefreshCon_Click(object sender, EventArgs e)
    {
        Response.Redirect("Opportunity_Edit.aspx?&Opportunity_Code=" + Request["Opportunity_Code"]);
    }


    protected void btnEditSubmit_ServerClick(object sender, System.EventArgs e)
    {
        

        try
        {
           
        }
        catch (Exception ex)
        {
           
            return;
        }
        try
        {
            string Company = "";
            string Division = "";
            string Center = "";
            string Stream = "";
            string app_no = "";
            string Flag = "";
            //Dim App_no As String = ""
            Company = ddltargetcompanyadd.SelectedValue;
            Division = ddltargetdivisionadd.SelectedValue;
            Center = ddltargetcenteradd1.SelectedValue;
            Stream = ddlproduct.SelectedValue;
            app_no = txtapplicationno.Text;
            string oppid1 = Request["Opportunity_Code"];
            Flag = ProductController.CheckDuplicateAppnoreturnoppid(Company, Division, Center, Stream, app_no);
            //Flag = "0"
            if (Flag == oppid1)
            {
                //Check for Which Student 
                //if student is same then ignore error else show error
                lblappnoerror.Visible = true;
                lblappnoerror.Text = "Application No. already exists!";
                return;
            }
            else
            {
                lblappnoerror.Visible = false;
            }


            string Gender = "";
            string Title = "";
            string Fname = "";
            string Mname = "";
            string Lname = "";
            string appno = "";
            decimal Score = 0;
            decimal Percentile = 0;
            int AreaRank = 0;
            int Overallrank = 0;
            string Scorerangetype = "";


            string handphone1 = "";
            string handphone2 = "";
            string landline = "";
            string emailid = "";
            string flatno = "";
            string buildingname = "";
            string Streetname = "";
            string Countryname = "";
            string State = "";
            string City = "";
            string Pincode = "";


            string category_TYpe_id = "";
            string category_Type = "";

            string Institutiontypeid = "";
            string InstituionTypedesc = "";
            string InstitutionName = "";
            string CurrentStandardid = "";
            string CurrentStandarddesc = "";
            string AdditionalDesc = "";
            string BoardUniversityid = "";
            string BoardUniversitydesc = "";
            string DivisionSectionGradeid = "";
            string DivisionSectionGradedesc = "";
            string Yearofpassingid = "";
            string Yearofpassingdesc = "";
            string Currentyearofeducationid = "";
            string Currentyearofeducationdesc = "";

            string Studentid = "";
            string LastcourseOpted = "";

            int Interested_Discipline_Id = 0;
            string Interested_Discipline_Desc = "";
            int Interested_Field_Id = 0;
            string Interested_Field_Desc = "";
            string CompetitiveExam = "";

            string Opp_type_id = "";
            string opp_name = "";
            string product_cat_id = "";
            string product_cat_desc = "";
            string sales_stage = "";
            DateTime join_date = default(DateTime);
            DateTime close_date = default(DateTime);
            string probability_percent = "";
            string next_step = "";
            decimal opp_value = 0;
            decimal discounts = 0;
            string discountnotes = "";
            discountnotes = txtdiscountnotes.Text;
            string Opp_Contact_SCompany = "";
            string Opp_Contact_SDivision = "";
            string Opp_Contact_SCenter = "";
            dynamic Opp_Contact_SZone = "";
            string Opp_Contact_TCompany = "";
            string Opp_Contact_TDivision = "";
            string Opp_Contact_TCenter = "";
            dynamic Opp_Contact_TZone = "";
            string Opp_Contact_Id = "";
            string Opp_Contact_Role = "";
            string Opp_Contact_Assignto = "";

            string opp_status = "";
            string opp_status_id = "";
            string opp_product = "";
            string opp_product_id = "";

            int MSmarkstotal = 0;
            int MSMarksobtained = 0;


            ////Gender = ddlgender.SelectedItem.Text
            //Title = ddltitle.SelectedItem.Text;
            //Fname = txtfirstname.Text;
            //Mname = txtmidname.Text;
            //Lname = txtlastname.Text;

            if (lblusercompany.Text == "MPUC")
            {
                Score = 0;
                Percentile = 0;
                AreaRank = 0;
                Overallrank = 0;
                Scorerangetype = "";
                Interested_Discipline_Id = 0;
                Interested_Discipline_Desc = "";
                Interested_Field_Id = 0;
                Interested_Field_Desc = "";
                CompetitiveExam = "";
                MSmarkstotal = 0;
                MSMarksobtained = 0;
            }
            else
            {
                
            }

            if (ddlstudenttypeadd.SelectedValue == "01")
            {
                Studentid = txtstudentid.Text;
               // LastcourseOpted = txtlastcourseopted.Text;
                Opp_Contact_SCompany = ddlsourcecompanyadd.SelectedValue;
                Opp_Contact_SDivision = ddlSourcedivisionadd.SelectedValue;
                Opp_Contact_SZone = ddlSourcezoneadd.SelectedValue;
                Opp_Contact_SCenter = ddlSourcecenteradd.SelectedValue;
                Opp_Contact_TCompany = ddltargetcompanyadd.SelectedValue;
                Opp_Contact_TDivision = ddltargetdivisionadd.SelectedValue;
                Opp_Contact_TZone = ddltargetzoneadd.SelectedValue;
                Opp_Contact_TCenter = ddltargetcenteradd1.SelectedValue;

            }
            else if (ddlstudenttypeadd.SelectedValue == "02")
            {
                Opp_Contact_TCompany = ddltargetcompanyadd.SelectedValue;
                Opp_Contact_TDivision = ddltargetdivisionadd.SelectedValue;
                Opp_Contact_TZone = ddltargetzoneadd.SelectedValue;
                Opp_Contact_TCenter = ddltargetcenteradd1.SelectedValue;
                Opp_Contact_SDivision = "";
                Opp_Contact_SZone = "";
                Opp_Contact_SCenter = "";

            }
            else if (ddlstudenttypeadd.SelectedValue == "03")
            {
                Studentid = txtstudentid.Text;
                //LastcourseOpted = txtlastcourseopted.Text;
                Opp_Contact_SCompany = ddlsourcecompanyadd.SelectedValue;
                Opp_Contact_SDivision = ddlSourcedivisionadd.SelectedValue;
                Opp_Contact_SZone = ddlSourcezoneadd.SelectedValue;
                Opp_Contact_SCenter = ddlSourcecenteradd.SelectedValue;
                Opp_Contact_TCompany = ddltargetcompanyadd.SelectedValue;
                Opp_Contact_TDivision = ddltargetdivisionadd.SelectedValue;
                Opp_Contact_TZone = ddltargetzoneadd.SelectedValue;
                Opp_Contact_TCenter = ddltargetcenteradd1.SelectedValue;

            }
            else if (ddlstudenttypeadd.SelectedValue == "04")
            {
                Opp_Contact_TCompany = ddltargetcompanyadd.SelectedValue;
                Opp_Contact_TDivision = ddltargetdivisionadd.SelectedValue;
                Opp_Contact_TZone = ddltargetzoneadd.SelectedValue;
                Opp_Contact_TCenter = ddltargetcenteradd1.SelectedValue;
                Opp_Contact_SDivision = "";
                Opp_Contact_SZone = "";
                Opp_Contact_SCenter = "";

            }
            else
            {
                Studentid = "";
                LastcourseOpted = "";
                Opp_Contact_SCompany = "";
                Opp_Contact_SDivision = "";
                Opp_Contact_SZone = "";
                Opp_Contact_SCenter = "";
                Opp_Contact_TCompany = "";
                Opp_Contact_TDivision = "";
                Opp_Contact_TZone = "";
                Opp_Contact_TCenter = "";

            }
            //Role = ddlrole.SelectedValue
            Opp_Contact_Assignto = txtassignedto.Text;
            //handphone1 = txthandphone1.Text;
            //handphone2 = txthandphone2.Text;
            //landline = txtlandline.Text;
            //emailid = txtemailid.Text;
            //flatno = txtflatno.Text;
            //buildingname = txtbuildingno.Text;
            //Streetname = txtstreetname.Text;
            //Countryname = ddlcountry.SelectedValue;
            //State = ddlstate.SelectedValue;
            //City = ddlcity.SelectedValue;
            //Pincode = txtpincode.Text;

            category_TYpe_id = ddlstudenttypeadd.SelectedValue;
            category_Type = ddlstudenttypeadd.SelectedItem.Text;

            //Institutiontypeid = ddlinstitutiontype.SelectedValue;
            //InstituionTypedesc = ddlinstitutiontype.SelectedItem.Text;
            //InstitutionName = txtnameofinstitution.Text;
            //CurrentStandardid = ddlcurrentstudying.SelectedValue;
            //CurrentStandarddesc = ddlcurrentstudying.SelectedItem.Text;
            //AdditionalDesc = txtadditiondesc.Text;
            //BoardUniversityid = ddlboard.SelectedValue;
            //BoardUniversitydesc = ddlboard.SelectedItem.Text;
            //DivisionSectionGradeid = ddlsection.SelectedValue;
            //DivisionSectionGradedesc = ddlsection.SelectedItem.Text;
            //Yearofpassingid = ddlyearofpassing.SelectedValue;
            //Yearofpassingdesc = ddlyearofpassing.SelectedItem.Text;
            Currentyearofeducationid = ddlacademicyear.SelectedValue;
            Currentyearofeducationdesc = ddlacademicyear.SelectedItem.Text;


            Opp_type_id = "";
            opp_name = "";
            product_cat_id = this.ddlAddProductCategory.SelectedValue;
            product_cat_desc = this.ddlAddProductCategory.SelectedItem.Text;
            sales_stage = this.ddlAddSalesStage.SelectedValue;
            if (string.IsNullOrEmpty(txtjoindate.Text))
            {
                txtjoindate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                join_date = Convert.ToDateTime(txtjoindate.Text, System.Globalization.CultureInfo.GetCultureInfo("Hi-IN").DateTimeFormat);
            }
            else
            {
                join_date = Convert.ToDateTime(txtjoindate.Text, System.Globalization.CultureInfo.GetCultureInfo("Hi-IN").DateTimeFormat);
            }
            close_date = Convert.ToDateTime(txtexpectedclosedate.Text, System.Globalization.CultureInfo.GetCultureInfo("Hi-IN").DateTimeFormat);
            probability_percent = txtprobabilitypercent.Text;
            next_step = "";
            opp_value = 0;
            discounts =Convert.ToDecimal(this.txtdiscount.Text);
            Opp_Contact_Assignto = this.txtassignedto.Text;
            opp_status = "";
            opp_status_id = "";
            opp_product = ddlproduct.SelectedItem.Text;
            opp_product_id = ddlproduct.SelectedValue;
            appno = this.txtapplicationno.Text;

            string SecContactType = "";
            string Secgender = "";
            string SecTitle = "";
            string SecFname = "";
            string SecMname = "";
            string SecLname = "";
            string Sechphone1 = "";
            string Sechphone2 = "";
            string Seclandline = "";
            string Secemail = "";
            string SecAdd1 = "";
            string Secadd2 = "";
            string SecStreetname = "";
            string SecCountryname = "";
            string SecStatename = "'";
            string SecCityname = "";
            string SecpostalCode = "";

            //SecContactType = ddlseccontacttype.SelectedValue;
            //SecTitle = ddlsectitle.SelectedItem.Text;
            //SecFname = txtsecfname.Text;
            //SecMname = txtsecmname.Text;
            //SecLname = txtseclname.Text;
            //Sechphone1 = txtsechandphone1.Text;
            //Sechphone2 = txtsechandphone2.Text;
            //Seclandline = txtseclandline.Text;
            //Secemail = txtsecemailid.Text;
            //SecAdd1 = txtsecaddress1.Text;
            //Secadd2 = txtsecaddress2.Text;
            //SecStreetname = txtsecStreetname.Text;
            //SecCountryname = ddlseccountry.SelectedValue;
            //SecStatename = ddlsecstate.SelectedValue;
            //SecCityname = ddlseccity.SelectedValue;
            //SecpostalCode = txtsecpincode.Text;

            string Seccondesc = "";
            //Seccondesc = ddlseccontacttype.SelectedItem.Text
            string Dob = "";
            string Examination = "";
            string Location = "";

            //Dob = txtdateofbirth.Text;
            //Examination = txtexaminationdetails.Text;
            //Location = ddllocation.SelectedValue;
            //Gender = ddlgenderadd.SelectedItem.Text;


            string Is_Branch_Topper = "";
            string Branch_Topper_Division = "";
            string Branch_Topper_Center = "";
            string Is_School_Ranker = "";
            string School_Name = "";
            string School_Division = "";
            string School_Rank = "";
            if (ckhBranchTopper.Checked == true)
            {
                Is_Branch_Topper = "1";
                Branch_Topper_Division = ddlbranchtopperdivision.SelectedValue;
                Branch_Topper_Center = ddlbranchtopperCenter.SelectedValue;
            }
            else
            {
                Is_Branch_Topper = "0";
                Branch_Topper_Division = "";
                Branch_Topper_Center = "";
            } 
            string Apply_Additional_Discount = "";
            string Discount_Type = "";


            if (chkSchoolRanker.Checked == true)
            {
                Is_School_Ranker = "1";
                School_Name = ddlschoolranker.SelectedValue;
                School_Division = txtschooldivision.Text;
                School_Rank = txtschoolrank.Text;
            }
            else
            {
                Is_School_Ranker = "0";
                School_Name = "";
                School_Division = "";
                School_Rank = "";
            }

            if (ckhRankerAdditional.Checked == true)
            {
                Apply_Additional_Discount = "1";
                Discount_Type = ddldiscountconditions.SelectedValue;
            }
            else
            {
                Apply_Additional_Discount = "0";
                Discount_Type = "";
            }

		    






            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];
            string UserName = cookie.Values["UserName"];
            if (Request["Opportunity_Code"] != null)
            {
                string oppid = Request["Opportunity_Code"];
                //update Opp
                string Con_id = ProductController.UpdateOpportunityContact_New(Title, Fname, Mname, Lname, Score, Percentile, AreaRank, Overallrank, Scorerangetype, handphone1,
                handphone2, landline, emailid, flatno, buildingname, Streetname, Countryname, State, City, Pincode,
                category_TYpe_id, category_Type, Institutiontypeid, InstituionTypedesc, InstitutionName, CurrentStandardid, CurrentStandarddesc, AdditionalDesc, BoardUniversityid, BoardUniversitydesc,
                DivisionSectionGradeid, DivisionSectionGradedesc, Yearofpassingid, Yearofpassingdesc, Currentyearofeducationid, Currentyearofeducationdesc, Studentid, LastcourseOpted, Opp_type_id, opp_name,
                product_cat_desc, product_cat_id, sales_stage, join_date, close_date, probability_percent, next_step, opp_value, discounts, Opp_Contact_SCompany,
                Opp_Contact_SDivision, Opp_Contact_SCenter, Opp_Contact_SZone, Opp_Contact_TDivision, Opp_Contact_TCenter, Opp_Contact_TZone, Opp_Contact_Role, Opp_Contact_Assignto, UserID, opp_status,
                opp_status_id, opp_product, opp_product_id, oppid, appno, SecContactType, SecTitle, SecFname, SecMname, SecLname,
                Sechphone1, Sechphone2, Seclandline, Secemail, SecAdd1, Secadd2, SecStreetname, SecCountryname, SecStatename, SecCityname,
                SecpostalCode, Seccondesc, Interested_Discipline_Id, Interested_Discipline_Desc, Interested_Field_Id, Interested_Field_Desc, CompetitiveExam, MSmarkstotal, MSMarksobtained, Opp_Contact_TCompany,
                Dob, Examination, Location, Gender, discountnotes,
                Is_Branch_Topper, Branch_Topper_Division, Branch_Topper_Center, Is_School_Ranker, School_Name, School_Division, School_Rank, Apply_Additional_Discount, Discount_Type,"101");

                InsertScore();
            }

            divSuccessmessage.Visible = true;
            lblsuccessMessage.Text = "Opportunity Updated Successfully";
            //Me.lblsuccessMessage.Focus()
            //Dim oppid As String = Request("Opportunity_Code")
            Bindlist();
           // BindSecContact();
//            BindScore();
            divErrormessage.Visible = false;
        }
        catch (Exception ex)
        {
            divErrormessage.Visible = true;
            lblerrormessage.Visible = true;
            lblerrormessage.Text = ex.Message;
        }
    }
    //private void BindScore()
    //{
    //    string Conid = lblprimarycontactid.Text;
    //    string Scoretypeid = "";
    //    string Score = "";
    //    HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
    //    string UserID = cookie.Values["UserID"];
    //    int Id = 0;
    //    DataSet ds = ProductController.GetAllScore(4, Conid, Scoretypeid, Score, UserID, Id);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        //dlScore.DataSource = ds;
    //        //dlScore.DataBind();
    //        //divscoreerror.Visible = false;
    //    }
    //    else
    //    {
    //        //divscoreerror.Visible = true;
    //        //lblscoreerror.Text = "No Scores Entered!";
    //    }
    //}

    private void InsertScore()
    {
        string Conid = lblprimarycontactid.Text;
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string User_id = cookie.Values["UserID"];
        object obj = null;
        Label lblscoredesc = default(Label);
        Label lblscoreid = default(Label);
        TextBox txtscore = default(TextBox);

        foreach (DataListItem li in dlScore.Items)
        {
            obj = li.FindControl("lblscoretypedesc");
            if (obj != null)
            {
                lblscoredesc = (Label)obj;
            }

            obj = li.FindControl("lblscoreid");
            if (obj != null)
            {
                lblscoreid = (Label)obj;
            }

            obj = li.FindControl("txtscore");
            if (obj != null)
            {
                txtscore = (TextBox)obj;
            }

            if (string.IsNullOrEmpty(txtscore.Text))
            {
            }
            else
            {
                ProductController.InsertScore(3, Conid, "", txtscore.Text, User_id, Convert.ToInt32(lblscoreid.Text));
            }

        }

    }

    //private void BindSecContact()
    //{
    //    if (Request["Opportunity_Code"] != null)
    //    {
    //        string Opporid = Request["Opportunity_Code"];

    //        DataSet ds = ProductController.Get_SecondaryContactbyOpporid(Opporid);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            dlseccontact.DataSource = ds;
    //            dlseccontact.DataBind();
    //            divseccontact.Visible = false;
    //        }
    //        else
    //        {
    //            divseccontact.Visible = true;
    //            lblseccontact.Text = "No Secondary Contact Found!";
    //        }
    //    }
    //}

    protected void dlseccontact_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            UpnlSecContact.Visible = true;
            UpnlAdd.Visible = false;
            lblprimaryconid.Text = e.CommandArgument.ToString();
            string Conid = lblprimaryconid.Text;
            BindSecContactDetails(Conid);
        }
    }

    protected void dlseccontact_ItemDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ScriptManager scriptManager__1 = ScriptManager.GetCurrent(this.Page);
            scriptManager__1.RegisterPostBackControl((LinkButton)e.Item.FindControl("lnkedit"));
        }
    }
    private void BindSecContactDetails(string Conid)
    {

        try
        {

            if (Request["Opportunity_Code"] != null)
            {
                string Con_id = Conid;
                SqlDataReader dr = ProductController.Get_SecondaryContactbyLeadidforfield1(Conid);
                if ((((dr) != null)))
                {
                    if (dr.Read())
                    {
                        //ContactType2();
                        if (dr["Con_type_id"].ToString() == "Select")
                        {
                            ddlseccontacttype2.SelectedIndex = 0;
                        }
                        else
                        {
                            ddlseccontacttype2.SelectedValue = dr["Con_type_id"].ToString();
                        }

                        if (dr["Con_title"].ToString() == "Mr.")
                        {
                            ddlsectitle2.SelectedValue = "1";
                        }
                        else if (dr["Con_title"].ToString() == "Ms.")
                        {
                            ddlsectitle2.SelectedValue = "2";
                        }
                        else
                        {
                            ddlsectitle2.SelectedIndex = 0;
                        }

                        if (dr["gender"].ToString() == "Male")
                        {
                            this.ddlsecgender.SelectedValue = "1";
                        }
                        else if (dr["gender"].ToString() == "Female")
                        {
                            this.ddlsecgender.SelectedValue = "2";
                        }
                        else
                        {
                            this.ddlsecgender.SelectedValue = "0";
                        }

                        txtsecfname2.Text = dr["Con_Firstname"].ToString();
                        txtsecmname2.Text = dr["Con_midname"].ToString();
                        txtseclname2.Text = dr["Con_lastname"].ToString();
                        txtsechandphone12.Text = dr["handphone1"].ToString();
                        txtsechandphone22.Text = dr["handphone2"].ToString();
                        txtseclandline2.Text = dr["landline"].ToString();
                        txtsecemailid2.Text = dr["Emailid"].ToString();
                        txtsecaddress12.Text = dr["Flatno"].ToString();
                        txtsecaddress22.Text = dr["BuildingName"].ToString();
                        txtsecStreetname2.Text = dr["StreetName"].ToString();
                        txtsecpincode2.Text = dr["Pincode"].ToString();

                        Institutetype();
                        if (dr["Institution_Type_Id"].ToString() == "Select")
                        {
                            ddlinstitutiontype2.SelectedIndex = 0;

                        }
                        else
                        {
                            ddlinstitutiontype2.SelectedValue = dr["Institution_Type_Id"].ToString();
                        }

                        txtnameofinstitution2.Text = dr["Institution_Description"].ToString();
                        CurrentStudyingIn1();
                        if (dr["Current_Standard_id"].ToString() == "Select")
                        {
                            ddlcurrentstudying2.SelectedIndex = 0;
                        }
                        else
                        {
                            ddlcurrentstudying2.SelectedValue = dr["Current_Standard_id"].ToString();
                        }

                        txtadditiondesc2.Text = dr["Additional_desc"].ToString();
                        Board();
                        if (dr["Board_id"].ToString() == "Select")
                        {
                            ddlboard2.SelectedIndex = 0;
                        }
                        else
                        {
                            ddlboard2.SelectedValue = dr["Board_id"].ToString();
                        }

                        DivisionSession();
                        if (dr["Section_id"].ToString() == "Select")
                        {
                            ddlsection2.SelectedIndex = 0;
                        }
                        else
                        {
                            ddlsection2.SelectedValue = dr["Section_id"].ToString();
                        }

                        Yearofpassing();
                        if (dr["Year_of_Passing_Id"].ToString() == "Select")
                        {
                            ddlyearofpassing2.SelectedIndex = 0;
                        }
                        else
                        {
                            ddlyearofpassing2.SelectedValue = dr["Year_of_Passing_Id"].ToString();
                        }

                        txtsecdob.Text = dr["dob"].ToString();
                        ddlseccountry2.SelectedValue = dr["Country"].ToString();
                        if (ddlseccountry2.SelectedValue == "Select")
                        {
                            ddlsecstate2.SelectedIndex = 0;
                            ddlseccity2.SelectedIndex = 0;
                        }
                        else
                        {
                            BindSecState();
                            ddlsecstate2.SelectedValue = dr["State"].ToString();
                            if (ddlsecstate2.SelectedValue == "Select")
                            {
                                ddlseccity2.SelectedIndex = 0;
                            }
                            else
                            {
                                BindSecCity();
                                ddlseccity2.SelectedValue = dr["City"].ToString();
                                if (ddlseccity2.SelectedValue == "Select")
                                {
                                    ddlseccity2.SelectedIndex = 0;
                                }
                                else
                                {
                                    ddlseccity2.SelectedValue = dr["City"].ToString();
                                    BindSecLocation();
                                    ddlSeclocation2.SelectedValue = dr["location_id"].ToString();
                                    if (ddlSeclocation2.SelectedValue == "Select")
                                    {
                                        ddlSeclocation2.SelectedIndex = 0;
                                    }
                                    else
                                    {
                                        
                                        ddlSeclocation2.SelectedValue = dr["location_id"].ToString();
                                    }
                                }

                            }
                        }

                    }
                }
            }
            divErrormessage.Visible = false;
        }
        catch (Exception ex)
        {
            divErrormessage.Visible = true;
            lblerrormessage.Visible = true;
            lblerrormessage.Text = ex.Message;
        }

    }
    private void CurrentStudyingIn1()
    {
        DataSet ds = ProductController.GetallCurrentStudyingin(ddlinstitutiontype2.SelectedValue);
        BindDDL(ddlcurrentstudying2, ds, "Description", "ID");
        this.ddlcurrentstudying2.Items.Insert(0, "Select");
        this.ddlcurrentstudying2.SelectedIndex = 0;
    }
    //private void ContactType2()
    //{
    //    DataSet ds = ProductController.GetAllSContactTypebyPContactType(ddlcontacttype1.SelectedValue);
    //    BindDDL(ddlseccontacttype2, ds, "Description", "ID");
    //    this.ddlseccontacttype2.Items.Insert(0, "Select");
    //    this.ddlseccontacttype2.SelectedIndex = 0;
    //}
    protected void ddlinstitutiontype2_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        DataSet ds = ProductController.GetallCurrentStudyingin(ddlinstitutiontype2.SelectedValue);
        BindDDL(ddlcurrentstudying2, ds, "Description", "ID");
        this.ddlcurrentstudying2.Items.Insert(0, "Select");
        this.ddlcurrentstudying2.SelectedIndex = 0;

        //Me.ddlinstitutiontype2.Focus()
    }
    //private void Bindboardbyid()
    //{
    //    DataSet ds = ProductController.GetallBoardbyinstitutetype(ddlinstitutiontype2.SelectedValue);
    //    BindDDL(ddlboard, ds, "Short_Description", "ID");
    //    ddlboard.Items.Insert(0, "Select");
    //    ddlboard.SelectedIndex = 0;
        //BindDDL(ddlboard2, ds, "Short_Description", "ID")
        //ddlboard2.Items.Insert(0, "Select")
        //ddlboard2.SelectedIndex = 0
    //}
    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }
    private void StudentType()
    {
        DataSet ds = ProductController.GetAllStudentType();
        BindDDL(ddlstudenttypeadd, ds, "Description", "Cust_Grp");
        ddlstudenttypeadd.Items.Insert(0, "Select");
        ddlstudenttypeadd.SelectedIndex = 0;
    }
    protected void ddlAddSalesStage_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        BindProbabilityPercent();
        //ddlAddSalesStage.Focus()
    }
    private void BindProbabilityPercent()
    {
        SqlDataReader dr = ProductController.GetProbabiltyPercent(ddlAddSalesStage.SelectedValue);
        if ((((dr) != null)))
        {
            if (dr.Read())
            {
                txtprobabilitypercent.Text = dr["Probability_Percent"].ToString();
            }
        }
    }
    //private void Discipline()
    //{
    //    DataSet ds = ProductController.GetallDiscipline();
    //    BindDDL(ddldiscipline, ds, "Discipline_Desc", "Discipline_Id");
    //    ddldiscipline.Items.Insert(0, "Select");
    //    ddldiscipline.SelectedIndex = 0;
    //}
    //private void Scorerange()
    //{
    //    DataSet ds = ProductController.GetScorerange();
    //    BindDDL(ddlscorerange, ds, "Description", "ID");
    //    ddlscorerange.Items.Insert(0, "Select");
    //    ddlscorerange.SelectedIndex = 0;
    //}
    
    //protected void ddldiscipline_SelectedIndexChanged(object sender, System.EventArgs e)
    //{
    //    if (ddldiscipline.SelectedIndex == 0)
    //    {
    //        ddlfieldint.Items.Insert(0, "Select");
    //        ddlfieldint.SelectedIndex = 0;
    //    }
    //    else
    //    {
    //        FieldInterested();
    //    }
    //    //ddldiscipline.Focus()
    //}
    //private void FieldInterested()
    //{
    //    DataSet ds = ProductController.GetAllFieldInterestedByDisciplineid(Convert.ToInt32 ( ddldiscipline.SelectedValue));
    //    BindDDL(ddlfieldint, ds, "IField_Desc", "C24_Ifieldid");
    //    ddlfieldint.Items.Insert(0, "Select");
    //    ddlfieldint.SelectedIndex = 0;
    //}
    private void Institutetype()
    {
        DataSet ds = ProductController.GetallInstituteType();
        //BindDDL(ddlinstitutiontype, ds, "Description", "ID");
        //ddlinstitutiontype.Items.Insert(0, "Select");
        //ddlinstitutiontype.SelectedIndex = 0;

        BindDDL(ddlinstitutiontype2, ds, "Description", "ID");
        ddlinstitutiontype2.Items.Insert(0, "Select");
        ddlinstitutiontype2.SelectedIndex = 0;
    }
    //protected void ddlinstitutiontype_SelectedIndexChanged(object sender, System.EventArgs e)
    //{
    //    DataSet ds = ProductController.GetallCurrentStudyingin(ddlinstitutiontype.SelectedValue);
    //    BindDDL(ddlcurrentstudying, ds, "Description", "ID");
    //    ddlcurrentstudying.Items.Insert(0, "Select");
    //    ddlcurrentstudying.SelectedIndex = 0;
    //    Bindboardbyid();
    //    //ddlinstitutiontype.Focus()
    //}
    //private void CurrentStudyingin()
    //{
    //    DataSet ds = ProductController.GetallCurrentStudyingin(ddlinstitutiontype.SelectedValue);
    //    BindDDL(ddlcurrentstudying, ds, "Description", "ID");
    //    ddlcurrentstudying.Items.Insert(0, "Select");
    //    ddlcurrentstudying.SelectedIndex = 0;
    //}

    private void Board()
    {
        DataSet ds = ProductController.GetallBoard();
        //BindDDL(ddlboard, ds, "Short_Description", "ID");
        //ddlboard.Items.Insert(0, "Select");
        //ddlboard.SelectedIndex = 0;

        BindDDL(ddlboard2, ds, "Short_Description", "ID");
        ddlboard2.Items.Insert(0, "Select");
        ddlboard2.SelectedIndex = 0;
    }

    private void Yearofpassing()
    {
        DataSet ds = ProductController.GetallYearofpassing();
        //BindDDL(ddlyearofpassing, ds, "Description", "ID");
        //ddlyearofpassing.Items.Insert(0, "Select");
        //ddlyearofpassing.SelectedIndex = 0;

        BindDDL(ddlyearofpassing2, ds, "Description", "ID");
        ddlyearofpassing2.Items.Insert(0, "Select");
        ddlyearofpassing2.SelectedIndex = 0;

    }

    private void DivisionSession()
    {
        DataSet ds = ProductController.GetAllDivisionSection();
        //BindDDL(ddlsection, ds, "Description", "ID");
        //ddlsection.Items.Insert(0, "Select");
        //ddlsection.SelectedIndex = 0;

        BindDDL(ddlsection2, ds, "Description", "ID");
        ddlsection2.Items.Insert(0, "Select");
        ddlsection2.SelectedIndex = 0;
    }
    private void ContactType()
    {
        DataSet ds = ProductController.GetallactiveContactType();
        //BindDDL(ddlcontacttype1, ds, "Description", "ID");
        //ddlcontacttype1.Items.Insert(0, "Select");
        //ddlcontacttype1.SelectedIndex = 0;
        BindDDL(ddlseccontacttype2, ds, "Description", "ID");

    }
    //protected void ddlcontacttype1_SelectedIndexChanged(object sender, System.EventArgs e)
    //{
    //    DataSet ds = ProductController.GetAllSContactTypebyPContactType(ddlcontacttype1.SelectedValue);
    //    BindDDL(ddlseccontacttype, ds, "Description", "ID");
    //    ddlseccontacttype.Items.Insert(0, "Select");
    //    ddlseccontacttype.SelectedIndex = 0;
    //    //ddlcontacttype1.Focus()
    //}
    //private void Contacttype1()
    //{
    //    DataSet ds = ProductController.GetAllSContactTypebyPContactType(ddlcontacttype1.SelectedValue);
    //    BindDDL(ddlseccontacttype, ds, "Description", "ID");
    //    ddlseccontacttype.Items.Insert(0, "Select");
    //    ddlseccontacttype.SelectedIndex = 0;
    //}
    private void BindSourceCompany()
    {
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];
        DataSet ds = ProductController.GetUser_Company_Division_Zone_Center(7, "", "", "", "");
        BindDDL(ddlsourcecompanyadd, ds, "Company_Name", "Company_Code");
        ddlsourcecompanyadd.Items.Insert(0, "Select");
        ddlsourcecompanyadd.SelectedIndex = 0;
        ddlSourcedivisionadd.Items.Insert(0, "Select");
        ddlSourcedivisionadd.SelectedIndex = 0;
        ddlSourcezoneadd.Items.Insert(0, "Select");
        ddlSourcezoneadd.SelectedIndex = 0;
        ddlSourcecenteradd.Items.Insert(0, "Select");
        ddlSourcecenteradd.SelectedIndex = 0;
    }
    private void BindTargetCompany()
    {
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];
        DataSet ds = ProductController.GetUser_Company_Division_Zone_Center(1, UserID, "", "", "");
        BindDDL(ddltargetcompanyadd, ds, "Company_Name", "Company_Code");
        ddltargetcompanyadd.Items.Insert(0, "Select");
        ddltargetcompanyadd.SelectedIndex = 0;
        ddltargetdivisionadd.Items.Insert(0, "Select");
        ddltargetdivisionadd.SelectedIndex = 0;
        ddltargetzoneadd.Items.Insert(0, "Select");
        ddltargetzoneadd.SelectedIndex = 0;
        ddltargetcenteradd1.Items.Insert(0, "Select");
        ddltargetcenteradd1.SelectedIndex = 0;
    }
    protected void ddlcompanyadd_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        BindSourceDivisionAdd();
        //ddlSourcedivisionadd.Focus()
    }
    private void BindSourceDivisionAdd()
    {
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];
        DataSet ds = ProductController.GetUser_Company_Division_Zone_Center(8, "", "", "", ddlsourcecompanyadd.SelectedValue);
        BindDDL(ddlSourcedivisionadd, ds, "Division_Name", "Division_Code");
        ddlSourcedivisionadd.Items.Insert(0, "Select");
        ddlSourcedivisionadd.SelectedIndex = 0;
    }

    protected void ddltargetcompanyadd_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        BindTargetDivisionadd();
        //ddltargetdivisionadd.Focus()
    }
    private void BindTargetDivisionadd()
    {
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];
        DataSet ds = ProductController.GetUser_Company_Division_Zone_Center(2, UserID, "", "", ddltargetcompanyadd.SelectedValue);
        BindDDL(ddltargetdivisionadd, ds, "Division_Name", "Division_Code");
        ddltargetdivisionadd.Items.Insert(0, "Select");
        ddltargetdivisionadd.SelectedIndex = 0;
    }
    protected void ddlSourcedivisionadd_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        BindSourceZoneAdd();
        //ddlSourcedivisionadd.Focus()
    }
    protected void ddltargetdivisionadd_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        BindTargetzoneadd();
        ddltargetdivisionadd.Focus();
        BindDiscountconditions();
    }
    private void BindSourceZoneAdd()
    {
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];
        DataSet ds = ProductController.GetUser_Company_Division_Zone_Center(9, "", ddlSourcedivisionadd.SelectedValue, "", ddlsourcecompanyadd.SelectedValue);
        BindDDL(ddlSourcezoneadd, ds, "Zone_Name", "Zone_Code");
        ddlSourcezoneadd.Items.Insert(0, "Select");
        ddlSourcezoneadd.SelectedIndex = 0;
    }
    private void BindTargetzoneadd()
    {
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];
        DataSet ds = ProductController.GetUser_Company_Division_Zone_Center(3, UserID, ddltargetdivisionadd.SelectedValue, "", ddltargetcompanyadd.SelectedValue);
        BindDDL(ddltargetzoneadd, ds, "Zone_Name", "Zone_Code");
        ddltargetzoneadd.Items.Insert(0, "Select");
        ddltargetzoneadd.SelectedIndex = 0;
    }
    protected void ddlSourcezoneadd_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        BindSourceCenterAdd();
        //ddlSourcecenteradd.Focus()
    }

    protected void ddltargetzoneadd_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        BindTargetCenterAdd();
        //ddltargetcenteradd.Focus()
    }
    private void BindSourceCenterAdd()
    {
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];
        DataSet ds = ProductController.GetUser_Company_Division_Zone_Center(10, "", ddlSourcedivisionadd.SelectedValue, ddlSourcezoneadd.SelectedValue, ddlsourcecompanyadd.SelectedValue);
        BindDDL(ddlSourcecenteradd, ds, "Center_name", "Center_Code");
        ddlSourcecenteradd.Items.Insert(0, "Select");
        ddlSourcecenteradd.SelectedIndex = 0;
    }
    private void BindTargetCenterAdd()
    {
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];
        DataSet ds = ProductController.GetUser_Company_Division_Zone_Center(4, UserID, ddltargetdivisionadd.SelectedValue, ddltargetzoneadd.SelectedValue, ddltargetcompanyadd.SelectedValue);
        BindDDL(ddltargetcenteradd1, ds, "Center_name", "Center_Code");
        ddltargetcenteradd1.Items.Insert(0, "Select");
        ddltargetcenteradd1.SelectedIndex = 0;
    }
    //private void Country()
    //{
    //    DataSet ds = ProductController.GetallCountry();
    //    BindDDL(ddlcountry, ds, "Country_Name", "Country_Code");
    //    ddlcountry.Items.Insert(0, "Select");
    //    ddlcountry.SelectedIndex = 0;
    //    ddlstate.Items.Insert(0, "Select");
    //    ddlstate.SelectedIndex = 0;
    //    ddlcity.Items.Insert(0, "Select");
    //    ddlcity.SelectedIndex = 0;
    //    ddllocation.Items.Insert(0, "Select");
    //    ddllocation.SelectedIndex = 0;
    //    BindDDL(ddlseccountry2, ds, "Country_Name", "Country_Code");
    //    ddlseccountry2.Items.Insert(0, "Select");
    //    ddlseccountry2.SelectedIndex = 0;
    //    ddlsecstate2.Items.Insert(0, "Select");
    //    ddlsecstate2.SelectedIndex = 0;
    //    ddlseccity2.Items.Insert(0, "Select");
    //    ddlseccity2.SelectedIndex = 0;
    //    ddlSeclocation2.Items.Insert(0, "Select");
    //    ddlSeclocation2.SelectedIndex = 0;

    //}
    //protected void ddlcountry_SelectedIndexChanged(object sender, System.EventArgs e)
    //{
    //    State();
    //    //ddlstate.Focus()
    //}
    //private void State()
    //{
    //    DataSet ds = ProductController.GetallStatebyCountry(ddlcountry.SelectedValue);
    //    BindDDL(ddlstate, ds, "State_Name", "State_Code");
    //    ddlstate.Items.Insert(0, "Select");
    //    ddlstate.SelectedIndex = 0;
    //}
    //protected void ddlstate_SelectedIndexChanged(object sender, System.EventArgs e)
    //{
    //    City();
    //    //ddlcity.Focus()
    //}
    //private void City()
    //{
    //    DataSet ds = ProductController.GetallCitybyState(ddlstate.SelectedValue);
    //    BindDDL(ddlcity, ds, "City_Name", "City_Code");
    //    ddlcity.Items.Insert(0, "Select");
    //    ddlcity.SelectedIndex = 0;
    //}
    //protected void ddlcity_SelectedIndexChanged(object sender, System.EventArgs e)
    //{
    //    BindLocation();
    //}
    //private void BindLocation()
    //{
    //    DataSet ds = ProductController.GetallLocationbycity(ddlcity.SelectedValue);
    //    BindDDL(ddllocation, ds, "Location_Name", "Location_Code");
    //    ddllocation.Items.Insert(0, "Select");
    //    ddllocation.SelectedIndex = 0;
    //}
    protected void ddlstudenttypeadd_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (ddlstudenttypeadd.SelectedValue == "01")
        {
            tblorgassign.Visible = true;
            tblrow1.Visible = true;
            trSourcecompany.Visible = true;
            //tdstudentid.Visible = true;
            //tdstudentid1.Visible = true;
            //tdlastcourse.Visible = true;
            //tdlastcourse1.Visible = true;
            ////ddlstudenttypeadd.Focus()
        }
        else if (ddlstudenttypeadd.SelectedValue == "02")
        {
            tblorgassign.Visible = true;
            tblrow1.Visible = false;
            trSourcecompany.Visible = false;
            //tdstudentid.Visible = false;
            //tdstudentid1.Visible = false;
            //tdlastcourse.Visible = false;
            //tdlastcourse1.Visible = false;
            ////ddlstudenttypeadd.Focus()
        }
        else if (ddlstudenttypeadd.SelectedValue == "03")
        {
            tblorgassign.Visible = true;
            tblrow1.Visible = true;
            trSourcecompany.Visible = true;
            //tdstudentid.Visible = true;
            //tdstudentid1.Visible = true;
            //tdlastcourse.Visible = true;
            //tdlastcourse1.Visible = true;
            ////ddlstudenttypeadd.Focus()
        }
        else if (ddlstudenttypeadd.SelectedValue == "04")
        {
            tblorgassign.Visible = true;
            tblrow1.Visible = false;
            trSourcecompany.Visible = false;
            //tdstudentid.Visible = false;
            //tdstudentid1.Visible = false;
            //tdlastcourse.Visible = false;
            //tdlastcourse1.Visible = false;
            ////ddlstudenttypeadd.Focus()
        }
        else
        {
            tblorgassign.Visible = false;
            tblrow1.Visible = true;
            //tdstudentid.Visible = false;
            //tdstudentid1.Visible = false;
            //tdlastcourse.Visible = false;
            //tdlastcourse1.Visible = false;
            ////ddlstudenttypeadd.Focus()
        }

    }
    protected void ddlseccountry_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        BindSecState();
        //ddlseccountry2.Focus()
    }
    private void BindSecState()
    {
        DataSet ds = ProductController.GetallStatebyCountry(ddlseccountry2.SelectedValue);
        BindDDL(ddlsecstate2, ds, "State_Name", "State_Code");
        ddlsecstate2.Items.Insert(0, "Select");
        ddlsecstate2.SelectedIndex = 0;
        ddlseccity2.SelectedIndex = 0;
    }
    protected void ddlSecstate_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        BindSecCity();
        //ddlsecstate2.Focus()
        ddlseccity2.SelectedIndex = 0;
    }
    private void BindSecCity()
    {
        DataSet ds = ProductController.GetallCitybyState(ddlsecstate2.SelectedValue);
        BindDDL(ddlseccity2, ds, "City_Name", "City_Code");
        ddlseccity2.Items.Insert(0, "Select");
        ddlseccity2.SelectedIndex = 0;
    }
    protected void ddlseccity2_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        BindSecLocation();
    }
    private void BindSecLocation()
    {
        DataSet ds = ProductController.GetallLocationbycity(ddlseccity2.SelectedValue);
        BindDDL(ddlSeclocation2, ds, "Location_Name", "Location_Code");
        ddlSeclocation2.Items.Insert(0, "Select");
        ddlSeclocation2.SelectedIndex = 0;
    }
    private void BindProductCategory()
    {
        DataSet ds = ProductController.GetallOpporProductCategory();
        BindDDL(ddlAddProductCategory, ds, "Description", "ID");
        ddlAddProductCategory.Items.Insert(0, "Select");
        ddlAddProductCategory.SelectedIndex = 0;
    }
    private void BindSalesStage()
    {
        DataSet ds = ProductController.GetallSalesStage();
        BindDDL(ddlAddSalesStage, ds, "Sales_Stage_Desc", "Sales_Id");
        ddlAddSalesStage.Items.Insert(0, "Select");
        ddlAddSalesStage.SelectedIndex = 0;
    }
    private void BindSalesChannel()
    {
        DataSet ds = ProductController.GetAllSalesChannel();
        BindDDL(ddlsaleschannel, ds, "Description", "ID");
        ddlsaleschannel.Items.Insert(0, "Select");
        ddlsaleschannel.SelectedIndex = 0;
    }
    //Protected Sub ddltargetcenter_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddltargetcenteradd.SelectedIndexChanged
    //    BindAcademicYear()
    //    'ddltargetcenteradd.Focus()
    //End Sub
    protected void ddltargetcenteradd1_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        BindAcademicYear();
    }

    private void BindAcademicYear()
    {
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];
        DataSet ds = ProductController.GetAcademicYearbyCenter(ddltargetcenteradd1.SelectedValue);
        BindDDL(ddlacademicyear, ds, "Acad_Year", "Acad_Year");
        ddlacademicyear.Items.Insert(0, "Select");
        ddlacademicyear.SelectedIndex = 0;
    }
    protected void ddlacademicyear_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        BindStream();
        //ddlacademicyear.Focus()
    }
    private void BindStream()
    {
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];
        DataSet ds = ProductController.GetStreamby_Center_AcademicYear(ddltargetcenteradd1.SelectedValue, ddlacademicyear.SelectedValue);
        BindDDL(ddlproduct, ds, "Stream_Sdesc", "Stream_Code");
        //ddlproduct.Items.Insert(0, "Select")
        //ddlproduct.SelectedIndex = 0
        //To Do
    }
    protected void btnclear_ServerClick(object sender, System.EventArgs e)
    {
        Response.Redirect("opportunity.aspx");
    }

    protected void btnSubmitSeccon_ServerClick(object sender, System.EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtsecdob.Text))
            {
            }
            else
            {
                if (Convert.ToDateTime(txtsecdob.Text) > DateTime.Today)
                {
                    Label24.Visible = true;
                    Label24.Text = "DOB cannot be a future date";
                    txtsecdob.Focus();
                    //lbldateerrorsubmit.Visible = False
                    return;
                }
                else
                {
                    //lbldateerrorsubmit.Visible = False
                    Label24.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Label24.Visible = true;
            Label24.Text = ex.Message;
            txtsecdob.Focus();
            return;
        }


        try
        {

            string SecContactTypeid = "";
            string Secgender = "";
            string SecTitle = "";
            string SecFname = "";
            string SecMname = "";
            string SecLname = "";
            string Sechphone1 = "";
            string Sechphone2 = "";
            string Seclandline = "";
            string Secemail = "";
            string SecAdd1 = "";
            string Secadd2 = "";
            string SecStreetname = "";
            string SecCountryname = "";
            string SecStatename = "'";
            string SecCityname = "";
            string SecpostalCode = "";
            string Secintitutiontype = "";
            string SecInstitutionname = "";
            string Secboard = "";
            string SecCurrstying = "";
            string SecDivision = "";
            string SecYOP = "";
            string SECAdditinalinfo = "";
            string Seccondesc = "";
            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];
            string UserName = cookie.Values["UserName"];

            string Institutiontypeid = "";
            string InstituionTypedesc = "";
            string InstitutionName = "";
            string CurrentStandardid = "";
            string CurrentStandarddesc = "";
            string AdditionalDesc = "";
            string BoardUniversityid = "";
            string BoardUniversitydesc = "";
            string DivisionSectionGradeid = "";
            string DivisionSectionGradedesc = "";
            string Yearofpassingid = "";
            string Yearofpassingdesc = "";
            string Currentyearofeducationid = "";
            string Currentyearofeducationdesc = "";

            string Conid = "";
            Conid = lblprimaryconid.Text;

            SecContactTypeid = ddlseccontacttype2.SelectedValue;
            Seccondesc = ddlseccontacttype2.SelectedItem.Text;
            SecTitle = ddlsectitle2.SelectedItem.Text;
            SecFname = txtsecfname2.Text;
            SecMname = txtsecmname2.Text;
            SecLname = txtseclname2.Text;
            Sechphone1 = txtsechandphone12.Text;
            Sechphone2 = txtsechandphone22.Text;
            Seclandline = txtseclandline2.Text;
            Secemail = txtsecemailid2.Text;
            SecAdd1 = txtsecaddress12.Text;
            Secadd2 = txtsecaddress22.Text;
            SecStreetname = txtsecStreetname2.Text;
            SecCountryname = ddlseccountry2.SelectedValue;
            SecStatename = ddlsecstate2.SelectedValue;
            SecCityname = ddlseccity2.SelectedValue;
            SecpostalCode = txtsecpincode2.Text;


            Institutiontypeid = ddlinstitutiontype2.SelectedValue;
            InstituionTypedesc = ddlinstitutiontype2.SelectedItem.Text;
            InstitutionName = txtnameofinstitution2.Text;
            CurrentStandardid = ddlcurrentstudying2.SelectedValue;
            CurrentStandarddesc = ddlcurrentstudying2.SelectedItem.Text;
            AdditionalDesc = txtadditiondesc2.Text;
            BoardUniversityid = ddlboard2.SelectedValue;
            BoardUniversitydesc = ddlboard2.SelectedItem.Text;
            DivisionSectionGradeid = ddlsection2.SelectedValue;
            DivisionSectionGradedesc = ddlsection2.SelectedItem.Text;
            Yearofpassingid = ddlyearofpassing2.SelectedValue;
            Yearofpassingdesc = ddlyearofpassing2.SelectedItem.Text;

            string Location = "";
            string Gender = "";
            string Dob = "";
            Location = ddlSeclocation2.SelectedValue;
            Gender = ddlsecgender.SelectedItem.Text;
            Dob = txtsecdob.Text;

            string Con_Id = ProductController.InsertSecondaryLeadContact("", Conid, Institutiontypeid, InstituionTypedesc, InstitutionName, CurrentStandardid, CurrentStandarddesc, AdditionalDesc, BoardUniversityid, BoardUniversitydesc,
            DivisionSectionGradeid, DivisionSectionGradedesc, Yearofpassingid, Yearofpassingdesc, Currentyearofeducationid, Currentyearofeducationdesc, SecContactTypeid, Seccondesc, SecTitle, SecFname,
            SecMname, SecLname, Sechphone1, Sechphone2, Seclandline, Secemail, SecAdd1, Secadd2, SecStreetname, SecCountryname,
            SecStatename, SecCityname, SecpostalCode, Location, Gender, Dob);
            string Opportunity_Code = Request["Opportunity_Code"];
            Response.Redirect("Opportunity_Edit.aspx?&Opportunity_Code=" + Opportunity_Code);
            divErrormessage.Visible = false;
        }
        catch (Exception ex)
        {
            divErrormessage.Visible = true;
            lblerrormessage.Visible = true;
            lblerrormessage.Text = ex.Message;
        }

    }

    protected void btnclearSeccon_ServerClick(object sender, System.EventArgs e)
    {
        string Opportunity_Code = Request["Opportunity_Code"];
        Response.Redirect("Opportunity_Edit.aspx?&Opportunity_Code=" + Opportunity_Code);
    }

    protected void btnaddcontact_ServerClick(object sender, System.EventArgs e)
    {
        string Opportunity_Code = Request["Opportunity_Code"];
        Response.Redirect("Contact_OAdd.aspx?&Opportunity_Code=" + Opportunity_Code);
    }


    protected void btnsearchoppor_ServerClick(object sender, System.EventArgs e)
    {
        Response.Redirect("Opportunity.aspx");
    }
    protected void ddlbranchtopperdivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindBranchTopperCenter();

    }
    protected void ckhBranchTopper_CheckedChanged(object sender, EventArgs e)
    {
        ddlbranchtopperdivision.SelectedIndex = 0;
        ddlbranchtopperCenter.Items.Clear();
        if (ckhBranchTopper.Checked)
        {
            trBranchTopper.Visible = true;
            
        }
        else
        {
            
            trBranchTopper.Visible = false;
        }


    }
    protected void chkSchoolRanker_CheckedChanged(object sender, EventArgs e)
    {

        ddlschoolranker.SelectedIndex = 0;
        txtschooldivision.Text = "";
        txtschoolrank.Text = "";
        if (chkSchoolRanker.Checked)
        {
            trSchoolRanker.Visible = true;
        }
        else
        {
            trSchoolRanker.Visible = false;
        }


    }
    protected void ckhRankerAdditional_CheckedChanged(object sender, EventArgs e)
    {
        ddldiscountconditions.SelectedIndex = 0;

        if (ckhRankerAdditional.Checked)
        {
            trDiscount.Visible = true;
        }
        else
        {
            trDiscount.Visible = false;
        }

    }

    private void BindBranchTopperDivision()
    {

        ddlbranchtopperCenter.Items.Clear();
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];
        DataSet ds = ProductController.GetUser_Company_Division_Zone_Center(8, UserID, "", "", "MT");
        BindDDL(ddlbranchtopperdivision, ds, "Division_Name", "Division_Code");
        ddlbranchtopperdivision.Items.Insert(0, "Select");
        ddlbranchtopperdivision.SelectedIndex = 0;

    }


    private void BindBranchTopperCenter()
    {
        ddlbranchtopperCenter.Items.Clear();

        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];

        string divcode = ddlbranchtopperdivision.SelectedValue;

        DataSet ds = ProductController.GetUser_Company_Division_Zone_Center(11, UserID, divcode, "", "MT");
        BindDDL(ddlbranchtopperCenter, ds, "center_name", "center_code");
        ddlbranchtopperCenter.Items.Insert(0, "Select");
        ddlbranchtopperCenter.SelectedIndex = 0;

    }


    private void BindddlInstitute()
    {
        DataSet ds = ProductController.GetallInstitutename();
        BindDDL(ddlschoolranker, ds, "Institution_Description", "Institution_Description");
        ddlschoolranker.Items.Insert(0, "Select");
        ddlschoolranker.SelectedIndex = 0;
    }

    private void BindDiscountconditions()
    {
        string division_Code = ddltargetdivisionadd.SelectedValue;
        string Company_Code = ddltargetcompanyadd.SelectedValue;
        DataSet ds = ProductController.GetDiscount_Types(3, division_Code, Company_Code);
        BindDDL(ddldiscountconditions, ds, "Discount_Type_Short_Desc", "Discount_Type");
        ddldiscountconditions.Items.Insert(0, "Select");
        ddldiscountconditions.SelectedIndex = 0;
    }


    private void Country2()
    {
        DataSet ds = ProductController.GetallCountry();
        BindDDL(ddlCountry, ds, "Country_Name", "Country_Code");
        ddlCountry.Items.Insert(0, "Select");
        ddlCountry.SelectedIndex = 0;
        ddlstate.Items.Insert(0, "Select");
        ddlstate.SelectedIndex = 0;
        ddlcity.Items.Insert(0, "Select");
        ddlcity.SelectedIndex = 0;
    }

    private void BindState()
    {
        DataSet ds = ProductController.GetallStatebyCountry(ddlCountry.SelectedValue);
        BindDDL(ddlstate, ds, "State_Name", "State_Code");
        ddlstate.Items.Insert(0, "Select");
        ddlstate.SelectedIndex = 0;
        ddlcity.Items.Clear();
        ddlcity.Items.Insert(0, "Select");
        ddlcity.SelectedIndex = 0;
    }

    private void BindCity()
    {
        DataSet ds = ProductController.GetallCitybyState(ddlstate.SelectedValue);
        BindDDL(ddlcity, ds, "City_Name", "City_Code");
        ddlcity.Items.Insert(0, "Select");
        ddlcity.SelectedIndex = 0;
    }

    protected void ddlstate_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        BindCity();
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        BindState();
    }

    private void ContactType3()
    {
        DataSet ds = ProductController.GetallactiveContactTypeinrelation();
        BindDDL(ddlContactType, ds, "Description", "ID");
    }



    private void BindLocation()
    {
        DataSet ds = ProductController.GetallLocationbycity(ddlcity.SelectedValue);
        BindDDL(ddllocation, ds, "Location_Name", "Location_Code");
        ddllocation.Items.Insert(0, "Select");
        ddllocation.SelectedIndex = 0;
    }  


    private void BindSecContactDetails2(string Conid)
    {
        string Con_id = Conid;

        //lblPKey_Con_Id.Text = Con_id;

        HtmlAnchor editContact = aedit;
        editContact.Visible = true;
        editContact.HRef = "Contact_Edit.aspx?&Con_id=" + Con_id;

        ContactInfoPanel1.BindSecContactDetails(Con_id);

        DataSet ds = ProductController.Get_ContactbyContactId(7, Con_id);

        if (ds.Tables[0].Rows.Count > 0)
        {
            if ((ds.Tables[0].Rows[0]["Contact_Source_Code"].ToString() == "Select") || (ds.Tables[0].Rows[0]["Contact_Source_Code"].ToString() == ""))
            {
                ddlContactsourceadd.SelectedIndex = 0;
            }
            else
            {
                ddlContactsourceadd.SelectedValue = ds.Tables[0].Rows[0]["Contact_Source_Code"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["Con_type_id"].ToString() == "Select") || (ds.Tables[0].Rows[0]["Con_type_id"].ToString() == ""))
            {
                ddlContactType.SelectedIndex = 0;
            }
            else
            {
                ddlContactType.SelectedValue = ds.Tables[0].Rows[0]["Con_type_id"].ToString();
            }

            if ((ds.Tables[0].Rows[0]["Category_Type_Id"].ToString() == "Select") || (ds.Tables[0].Rows[0]["Category_Type_Id"].ToString() == ""))
            {
                ddlstudenttypeadd.SelectedIndex = 0;
            }
            else
            {
                ddlstudenttypeadd.SelectedValue = ds.Tables[0].Rows[0]["Category_Type_Id"].ToString();
            }


            if (ds.Tables[0].Rows[0]["Con_title"].ToString() == "Mr.")
            {
                ddlTitle.SelectedValue = "1";
            }
            else if (ds.Tables[0].Rows[0]["Con_title"].ToString() == "Ms.")
            {
                ddlTitle.SelectedValue = "2";
            }
            else
            {
                ddlTitle.SelectedIndex = 0;
            }

            txtFirstName.Text = ds.Tables[0].Rows[0]["Con_Firstname"].ToString();
            txtMidName.Text = ds.Tables[0].Rows[0]["Con_midname"].ToString();
            txtLastName.Text = ds.Tables[0].Rows[0]["Con_lastname"].ToString();

            if ((ds.Tables[0].Rows[0]["Gender"].ToString() == "Select") || (ds.Tables[0].Rows[0]["Gender"].ToString() == ""))
            {
                ddlGender.SelectedIndex = 0;
            }
            else
            {
                if (ds.Tables[0].Rows[0]["Gender"].ToString() == "Male")
                {
                    ddlGender.SelectedValue = "1";
                }
                else if (ds.Tables[0].Rows[0]["Gender"].ToString() == "Female")
                {
                    ddlGender.SelectedValue = "2";
                }
                else
                    ddlGender.SelectedIndex = 0;
            }

            if (ds.Tables[0].Rows[0]["DOB"].ToString() == "")
            {
                txtdateofbirth.Text = "";
            }
            else
            {
                txtdateofbirth.Text = ds.Tables[0].Rows[0]["DOB"].ToString();
            }

            txtHandPhone1.Text = ds.Tables[0].Rows[0]["handphone1"].ToString();
            txtHandphone2.Text = ds.Tables[0].Rows[0]["handphone2"].ToString();
            txtlandline.Text = ds.Tables[0].Rows[0]["landline"].ToString();
            txtemailid.Text = ds.Tables[0].Rows[0]["Emailid"].ToString();
            txtaddress1.Text = ds.Tables[0].Rows[0]["Flatno"].ToString();
            txtaddress2.Text = ds.Tables[0].Rows[0]["BuildingName"].ToString();
            txtStreetname.Text = ds.Tables[0].Rows[0]["StreetName"].ToString();
            txtpincode.Text = ds.Tables[0].Rows[0]["Pincode"].ToString();

            if ((ds.Tables[0].Rows[0]["Country"].ToString() == "Select") || (ds.Tables[0].Rows[0]["Country"].ToString() == ""))
            {
                ddlCountry.SelectedIndex = 0;
                ddlstate.Items.Clear();
                ddlcity.Items.Clear();
                ddllocation.Items.Clear();
                ddlstate.Items.Insert(0, "Select");
                ddlcity.Items.Insert(0, "Select");
                ddllocation.Items.Insert(0, "Select");
                ddlstate.SelectedIndex = 0;
                ddlcity.SelectedIndex = 0;
                ddllocation.SelectedIndex = 0;
            }
            else
            {
                ddlCountry.SelectedValue = ds.Tables[0].Rows[0]["Country"].ToString();
                BindState();
                if ((ds.Tables[0].Rows[0]["State"].ToString() == "Select") || (ds.Tables[0].Rows[0]["State"].ToString() == ""))
                {
                    ddlstate.SelectedIndex = 0;
                    ddlcity.Items.Clear();
                    ddllocation.Items.Clear();
                    ddlcity.Items.Insert(0, "Select");
                    ddlcity.SelectedIndex = 0;
                    ddllocation.Items.Insert(0, "Select");
                    ddllocation.SelectedIndex = 0;
                }
                else
                {
                    ddlstate.SelectedValue = ds.Tables[0].Rows[0]["State"].ToString();
                    BindCity();
                    if ((ds.Tables[0].Rows[0]["City"].ToString() == "Select") || (ds.Tables[0].Rows[0]["City"].ToString() == ""))
                    {
                        ddlcity.SelectedIndex = 0;
                        ddllocation.Items.Clear();
                        ddllocation.Items.Insert(0, "Select");
                        ddllocation.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlcity.SelectedValue = ds.Tables[0].Rows[0]["City"].ToString();
                        BindLocation();
                        if ((ds.Tables[0].Rows[0]["Location"].ToString() == "Select") || (ds.Tables[0].Rows[0]["Location"].ToString() == ""))
                        {
                            ddllocation.SelectedIndex = 0;
                        }
                        else
                        {
                            ddllocation.SelectedValue = ds.Tables[0].Rows[0]["Location"].ToString();
                        }
                    }
                }
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                dlAcadInfo.Visible = true;
                lblAcadInfoRecord.Visible = false;
                dlAcadInfo.DataSource = ds.Tables[1];
                dlAcadInfo.DataBind();
            }
            else
            {
                dlAcadInfo.Visible = false;
                lblAcadInfoRecord.Visible = true;
                lblAcadInfoRecord.Text = "No records found..!";
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                dlSec_Con_Info.Visible = true;
                lblSecConRecord.Visible = false;
                dlSec_Con_Info.DataSource = ds.Tables[2];
                dlSec_Con_Info.DataBind();
            }
            else
            {
                dlSec_Con_Info.Visible = false;
                lblSecConRecord.Visible = true;
                lblSecConRecord.Text = "No records found..!";
            }
        }
    }

    protected void btnrefersh_ServerClick(object sender, System.EventArgs e)
    {
        //Response.Redirect("Convert_Lead_To_Opportunity.aspx?&Lead_Code=" + Request["Lead_Code"].ToString());
        Response.Redirect("Opportunity_Edit.aspx?&Opportunity_Code=" + Request["Opportunity_Code"]);
    }
}