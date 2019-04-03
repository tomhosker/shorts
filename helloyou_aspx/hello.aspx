<%@ Page Language="C#" %>
<html>
<head>
<title>Hello, you!</title>
</head>
<body>

<h1> Hello, you! </h1>

<p> Hello, you! Type your name below, and I'll say hello properly. </p>

<form method="get" action="hello.aspx">
Name: <input type="text" name="yname"><br/><br/>
<input type="submit" value="Submit">
</form>

<br/><br/>

<%
string yname = Request.QueryString["yname"];
if(String.IsNullOrEmpty(yname)) yname = "you";
string message = "Hello, "+yname+"!";
Response.Write(message);
%>

</body>
</html>
