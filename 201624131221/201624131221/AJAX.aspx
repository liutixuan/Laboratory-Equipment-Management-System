<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AJAX.aspx.cs" Inherits="_201624131221.third" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style2 {
            text-align: center;
        }
        .auto-style3 {
            text-align: center;
            width: 1339px;
        }
        .auto-style4 {
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    UpdatePanel外面,现在时间:<%=DateTime.Now.ToString() %>
    <br /><br /><div class="auto-style7">
                <div class="auto-style2">
                    <div class="auto-style3">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="查询" Font-Size="X-Large"/>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" Text="插入" Font-Size="X-Large" OnClick="Button2_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button3" runat="server" Text="修改" Font-Size="X-Large" OnClick="Button3_Click" />
            &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button4" runat="server" Font-Size="X-Large" Text="删除" OnClick="Button4_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </div>
                    <hr />
                    <div class="auto-style3">
                        <div class="auto-style3">
                            <div class="auto-style4">
                            请输入设备编号或设备名称查询:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 设备编号：<asp:TextBox ID="txtId" runat="server"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp; 设备名称：<asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                    &nbsp;&nbsp;&nbsp;&nbsp; 生产厂家：&nbsp;&nbsp; 
                                        <asp:TextBox ID="txtcompany" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="auto-style7">
                            <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;登记日期：<asp:TextBox ID="txtdatetime" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp; 维修次数：<asp:TextBox ID="txtdegree" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp; 设备负责人：<asp:TextBox ID="txtpeople" runat="server"></asp:TextBox>
                            <br />
                        </div>
                    </div>
                </div>
            </div>
            <hr  />
            <asp:Label ID="Label1" runat="server"></asp:Label>
           
            <br />
           
        <br />
            <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" Width="1339px" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" CellSpacing="1" GridLines="None">
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#594B9C" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#33276A" />
            </asp:GridView><br />
        <hr />
           设备信息表：<br />
        <br /><asp:GridView ID="GridView5" runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" Width="1339px" OnSelectedIndexChanged="GridView5_SelectedIndexChanged">
               <AlternatingRowStyle BackColor="White" />
               <FooterStyle BackColor="#CCCC99" />
               <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
               <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
               <RowStyle BackColor="#F7F7DE" />
               <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
               <SortedAscendingCellStyle BackColor="#FBFBF2" />
               <SortedAscendingHeaderStyle BackColor="#848384" />
               <SortedDescendingCellStyle BackColor="#EAEAD3" />
               <SortedDescendingHeaderStyle BackColor="#575357" />
           </asp:GridView>
            <br /><hr />
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            UpdatePanel里面,现在时间:<%=DateTime.Now.ToString() %><br /><br /><div class="auto-style3">
            <br />
                <br />
                <div class="auto-style2">
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button6" runat="server" OnClick="Button6_Click" Text="查询" Font-Size="X-Large"  />
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button7" runat="server" OnClick="Button7_Click" Text="插入" Font-Size="X-Large" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button8" runat="server" OnClick="Button8_Click" Text="修改" Font-Size="X-Large"  />
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button9" runat="server" OnClick="Button9_Click" Text="删除" Font-Size="X-Large"  />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                      </div>
                <hr /> 
                <div class="auto-style10">
                    <div class="auto-style4">
                    请输入设备编号或借出时间或借用人姓名查询：&nbsp;&nbsp;&nbsp; 设备编号：<asp:TextBox ID="txtId1" runat="server"></asp:TextBox>
               &nbsp;&nbsp;&nbsp;&nbsp;借出时间(xx-xx-xx): <asp:TextBox ID="txttime" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;借用人姓名: <asp:TextBox ID="txtbName" runat="server"></asp:TextBox>
                        <br />
                        <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 设备负责人：<asp:TextBox ID="txtpl" runat="server"></asp:TextBox>
                        &nbsp;&nbsp; &nbsp;&nbsp;是否已还:&nbsp;<asp:DropDownList ID="DropDownList1" runat="server" Height="30px" Width="50px">
                            <asp:ListItem Value="yes">是</asp:ListItem>
                            <asp:ListItem Value="no">否</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp;<br />
                <br />
                    </div>
                <hr />                       
                <div class="auto-style4">                            
            <br />
                    <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label>
                    <br />
                    <br />
            <br />
                    <asp:GridView ID="GridView3" runat="server" Width="1339px" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" OnSelectedIndexChanged="GridView3_SelectedIndexChanged" CellSpacing="1" GridLines="None">
                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#594B9C" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#33276A" />
                    </asp:GridView><hr />
                    设备借还信息表：<br />
            <br /><asp:GridView ID="GridView6" runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" Width="1339px" OnSelectedIndexChanged="GridView6_SelectedIndexChanged">
                        <AlternatingRowStyle BackColor="White" />
                        <FooterStyle BackColor="#CCCC99" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                        <RowStyle BackColor="#F7F7DE" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                        <SortedAscendingHeaderStyle BackColor="#848384" />
                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                        <SortedDescendingHeaderStyle BackColor="#575357" />
           </asp:GridView>
            <br />
               </div>
            </div>
        </div>

            
        </ContentTemplate>
    </asp:UpdatePanel>
     
    <br />


</asp:Content>
