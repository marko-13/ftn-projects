$(document).ready(function(){
	$('#forma').submit(function(event){
		event.preventDefault();
		
		let username=$('input[name="username"]').val();
		let password=$('input[name="password"]').val();
		let firstN = $('input[name="firstName"]').val();
		let lastN = $('input[name="lastName"]').val();
		let phone = $('input[name="phone"]').val();
		let city = $('input[name="city"]').val();
		let email =$('input[name="email"]').val();
		
		console.log('username:', username);
		
		$.post({
			url:'rest/register',
			data: JSON.stringify({"username":username, "password":password, "firstName":firstN, "lastName":lastN,
					"role":"Kupac", "phone":phone, "city":city, "email":email, "date":"50", "userRole":"1"
				}),
			contentType: 'application/json',
			success:function(product){
				alert("Uspesna registracija");
				window.location='./webshop.html';
			},
			error:function(message){
				alert("Greska")
			}
		});
	});
});