<%@ Page Language="C#" CodeBehind="Calculator.aspx.cs" Inherits="Calculator._Default"
    AutoEventWireup="true" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Calculator Example</title>
</head>
<body style="font-family: Tahoma, Garamond">
    <form id="form1" runat="server">
    <style>
        .rightAlign
        {
            text-align: right;            
            padding-right: 10px;
        }
        .button
        {
            font-size: Medium;
            font-weight: bold;
            height: 50px;
            width: 50px;
        }
        .number
        {
            color: #0000CC;
        }
        .function
        {
            color: #CC0000;
        }
        .operationButtons
        {
         width: 115px;
         font-weight: bold;         
        }
    </style>
    
    <script type="text/javascript">
        var sessionExpiration = function() {
            alert("Your session has timed out.  Please click ok to download your calculation history.");
            sendHistoryToClient();
        };
        var sendHistoryToClient = function() {
            window.open("CalculationHistory.aspx");
        };
        var factorTextClientID = '<%=(FactorText.ClientID)%>';
        var pageLoad = function() {
            // its ok to swallow the error - we error if the control is not visible, in which case we wouldn't want it to have focus anyways
            try {
                $get(factorTextClientID).focus();
            }
            catch (error) { }
        };
        // handles keypress of common calculation keys
        var firstKeyPressed = true;
        var firstLoad = <%=((!IsPostBack).ToString().ToLower())%>;
               
        var handleKeypress = function(e) {
            var evt = e || window.event;
            var character = String.fromCharCode(evt.which || evt.keyCode);
            return handleCharacter(character);
        };
        document['onkeypress'] = handleKeypress;
        var handleClick = function(button){
            // firefox needs us to walk the dom a bit
            var textContent = button.childNodes[0].textContent;
            // but IE just allows us to take innerText
            var character = textContent ? textContent.trim() : button.innerText;
            return handleCharacter(character);
        } 
        String.prototype.startsWith = function(str) {return (this.match("^"+str)==str)}       
        var handleCharacter = function(character) {
            var factorText = $get(factorTextClientID);
            var factor = factorText.value;
            if ((factor == "0") || (!charactersHaveBeenHandledThisPost))
            {
                // we want a leading zero
                if (!((factor == "0") && (character == ".")))
                {
                    factor = "";
                }
            }
            if ("+-/\*".indexOf(character, 0) > -1) {
                var actionDropDown = $get('<%=(ActionDropDown.ClientID)%>');
                // I don't like having the values here, but is there a better way?                                            
                setToSelectedValue(actionDropDown, character);
            }
            // get the result if enter or = was pressed
            // also get the result if the session has never posted back and we need to set the initial factor
            if (((character == "=") || (character.charCodeAt(0) == 13)) || 
                     ((firstLoad) && ("+-/\*=".indexOf(character, 0) > -1)))
            {   
                __doPostBack('','');
            }
            else
            {
            // add the character if we have room.  we have room for one extra character when we have the negative sign
            var characterShouldBeHandled = (("+-/\*=".indexOf(character, 0) == -1)  && 
                    ((factor.length < 5) || 
                     (factor.length == 5 && factor.startsWith("-"))  || 
                     (factor.length == 6 && factor.startsWith("0.")) ||  
                     (factor.length == 7 && factor.startsWith("-0."))));
            if (characterShouldBeHandled) 
                {
                    factor += character;   
                    charactersHaveBeenHandledThisPost = characterShouldBeHandled;         
                }
            }            
            factorText.value = factor;
            return false;            
        };  
        var switchSign = function() {
            var factorText = $get(factorTextClientID);
            var text = factorText.value;
            // we don't do for zero, doesn't make sense to have a signed zero
            if (text != "0" && text.length > 0)
            {
                if (text[0] == "-") {
                    // take off negative sign
                    text = text.substring(1, text.length);
                }
                else { // add negative sign
                    text = "-" + text;
                }
            }
            factorText.value = text;
            return false;
        };

        var buttonPress = function(button) {
            handleCharacter(button.value);
        };

        var setToSelectedValue = function(dropDown, value) {
            for (i = 0; i < dropDown.length; i++) {
                dropDown.options[i].selected = (value == dropDown.options[i].text);
            }
        };      
    </script>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                <asp:TabPanel ID="CalculatorTab" runat="server" HeaderText="Calculator">
                    <HeaderTemplate>
                        Calculator
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:Panel ID="CalculatorPanel" runat="server" Height="500px">
                            <asp:Label ID="InstructionsLabel" runat="server">
                                    Please select a mathematical function from those available below, enter a value, and click either equals or Answer
                            </asp:Label>
                            <br />
                            <br />
                            <asp:Label ID="ErrorMessage" Visible="False" runat="server" Font-Bold="True" Font-Underline="True"
                                ForeColor="Red"></asp:Label>
                            <br />
                            <br />
                            <asp:Panel BorderStyle="Solid" runat="server" ID="Calculator" Width="400px" Height="400px">
                                <br /><br />
                                <table style="margin-left: auto; margin-right: auto; margin-bottom: auto; margin-top: auto;">
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ActionDropDown" runat="server" DataTextField="Action" DataValueField="ID" />
                                        </td>
                                        <td>                                            
                                            <input runat="server" id="FactorText" type="text" value="0" maxlength="5" 
                                                class="rightAlign" style="width:75px;"/> 
                                        </td>
                                    </tr>
                                </table>
                                <table style="margin-left: auto; margin-right: auto; margin-bottom: auto; margin-top: auto;">
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button ID="NewSessionButton" runat="server" Font-Bold="True" OnClick="NewSession" CssClass="operationButtons"
                                                Text="Clear History" />
                                        </td>
                                        <td colspan="2">
                                            <button onclick="javascript:$get(factorTextClientID).value = '0'; return false;" class="operationButtons">Clear Value</button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <button id="Button7" class="number button" type="button" onclick="javascript:return handleClick(this);">
                                                7</button>
                                        </td>
                                        <td>
                                            <button id="Button8" class="number button" type="button" onclick="javascript:return handleClick(this);">
                                                8</button>
                                        </td>
                                        <td>
                                            <button id="Button9" class="number button" type="button" onclick="javascript:return handleClick(this);">
                                                9</button>
                                        </td>
                                        <td>
                                            <button id="Divide" class="function button" type="button" onclick="javascript:return handleClick(this);">
                                                /</button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <button id="Button4" class="number button" type="button" onclick="javascript:return handleClick(this);">
                                                4</button>
                                        </td>
                                        <td>
                                            <button id="Button5" class="number button" type="button" onclick="javascript:return handleClick(this);">
                                                5</button>
                                        </td>
                                        <td>
                                            <button id="Button6" class="number button" type="button" onclick="javascript:return handleClick(this);">
                                                6</button>
                                        </td>
                                        <td>
                                            <button id="Multiply" class="function button" type="button" onclick="javascript:return handleClick(this);">
                                                *</button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <button id="Button1" class="number button" type="button" onclick="javascript:return handleClick(this);">
                                                1</button>
                                        </td>
                                        <td>
                                            <button id="Button2" class="number button" type="button" onclick="javascript:return handleClick(this);">
                                                2</button>
                                        </td>
                                        <td>
                                            <button id="Button3" class="number button" type="button" onclick="javascript:return handleClick(this);">
                                                3</button>
                                        </td>
                                        <td>
                                            <button id="Subtract" class="function button" type="button" onclick="javascript:return handleClick(this);">
                                                -</button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <button id="Button0" class="number button" type="button" onclick="javascript:return handleClick(this);">
                                                0</button>
                                        </td>
                                        <td>
                                            <button id="SwitchSign" class="function button" type="button" onclick="javascript:return switchSign();">
                                                +/-</button>
                                        </td>
                                        <td>
                                            <button id="Decimal" class="number button" type="button" onclick="javascript:return handleClick(this);">
                                                .</button>
                                        </td>
                                        <td>
                                            <button id="Addition" class="function button" type="button" onclick="javascript:return handleClick(this);">
                                                +</button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button ID="Enter" runat="server" Font-Bold="True" Text="Enter" CommandName="Calculate" CssClass="operationButtons" />
                                        </td>
                                        <td colspan="2">
                                            <asp:Button ID="Equals" runat="server" Font-Bold="True" Text="="  CommandName="Calculate" CssClass="operationButtons" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="LogTab" runat="server" HeaderText="Log" Height="500px">
                    <ContentTemplate>
                        <asp:Panel ID="HistoryPanel" Height="500px" runat="server">
                            <asp:Label ID="CaculationXMLLabel" runat="server"></asp:Label>
                            <br />
                            <br />
                            <asp:LinkButton ID="DownloadXMLButton" runat="server" Text="Download Calculation History"
                                OnClientClick="sendHistoryToClient();" />
                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
            <script type="text/javascript">
                var charactersHaveBeenHandledThisPost = false;              
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
