<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="access-denied.aspx.cs" Inherits="AdminPanel_access_denied" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" Runat="Server">
   <br /><br />
            <div class="row">
                <div class="  col-sm-6 col-sm-offset-3 col-xs-offset-1">
                    <div class="text-center animated fadeInDown">
                        <ul class="list-unstyled">
                        	<li class="text-transparent padding-bottom animated shake"><div class="huge-text">401.1</div></li>
                            <li>
                            	<h2 class="text-center">Oops! You don't have access to this page. Contact support for further assistance.</h2>
                           </li>
                        </ul>
                        <br />
                    <%--    <div class="row">
                            <div class="btn-group btn-group-lg ">
                                 <button type="button" class="btn btn-primary pull-left"><i class="icon-envelope text-transparent"></i></button>
                                 <button type="button" class="btn btn-primary">Contact Support </button>
                            </div>
                        </div>--%>
                    </div>
                    
                </div>
            </div>
        
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" Runat="Server">
</asp:Content>

