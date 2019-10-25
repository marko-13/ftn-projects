function doCheckFilledLogin(){
	console.log('usao u fun1');
	var allFilled = true;
	
	if($('#username').val()==''){
		allFiled=false;
		return false;
	}
	if($('#password').val()==''){
		allFilled=false;
		return false;
	}
	$('#btn_login').removeAttr("disabled");
}

function doCheckFilledRegister(){
	console.log('usao u fun2');
	var allFilled = true;
	
	if($('#reg_username').val()==''){
		allFilled=false;
		return false;
	}
	
	if($('#reg_password').val()==''){
		allFilled=false;
		return false;
	}
	if($('#reg_firstN').val()==''){
		allFilled=false;
		return false;
	}
	if($('#reg_lastN').val()==''){
		allFilled=false;
		return false;
	}
	if($('#reg_phone').val()==''){
		allFilled=false;
		return false;
	}
	if($('#reg_city').val()==''){
		allFilled=false;
		return false;
	}
	if($('#reg_email').val()==''){
		allFilled=false;
		return false;
	}
		
	$('#btn_register').removeAttr("disabled");
}

//***************************************************
//MAIN
//***************************************************
$(document).ready(function() {
	
	//Da se btn_login enable samo ako je nesto uneto i u polje username i u polje sifra
	$('#username').keyup(doCheckFilledLogin).focusout(doCheckFilledLogin);
	$('#password').keyup(doCheckFilledLogin).focusout(doCheckFilledLogin);
	
	//Da se btn_register enable samo ako je nesto u svim poljima
	$('#reg_username').keyup(doCheckFilledRegister).focusout(doCheckFilledRegister);
	$('#reg_password').keyup(doCheckFilledRegister).focusout(doCheckFilledRegister);
	$('#reg_firstN').keyup(doCheckFilledRegister).focusout(doCheckFilledRegister);
	$('#reg_lastN').keyup(doCheckFilledRegister).focusout(doCheckFilledRegister);
	$('#reg_phone').keyup(doCheckFilledRegister).focusout(doCheckFilledRegister);
	$('#reg_city').keyup(doCheckFilledRegister).focusout(doCheckFilledRegister);
	$('#reg_email').keyup(doCheckFilledRegister).focusout(doCheckFilledRegister);

	
	$('#btn_login').click(function() {
		
		let username = $('#username').val();
		let password = $('#password').val();

		console.log(password);
		$.post({
			//products
			url: 'rest/login',
			data: JSON.stringify({username: username, password: password}),
			contentType: 'application/json',
			success: function(message) {
				alert('Uspesno logovanje');
				window.location='./webshop.html';
			},
			error: function(message) {
				alert('Neispravni podaci')
			}
		});
	});
	
	$('#btn_guest').click(function(){
		window.location='./webshop.html';
	});
	
	
	//ZATO STO JE FORMA MORA PREKO SUBMIT NE MOZE PREKO BUTTONA
	$('#forma').submit(function(event){
		event.preventDefault();
		
		let username=$('#reg_username').val();
		let password=$('#reg_password').val();
		let firstN = $('#reg_firstN').val();
		let lastN = $('#reg_lastN').val();
		let phone = $('#reg_phone').val();
		let city = $('#reg_city').val();
		let email =$('#reg_email').val();
		
		let date = new Date();
		
		//console.log('username:', username);
		//console.log('city: ', city);
		//alert('cekaj')
		
		$.post({
			url:'rest/register',
			data: JSON.stringify({"username":username, "password":password, "firstName":firstN, "lastName":lastN,
					"role":"Kupac", "phone":phone, "city":city, "email":email, "date":date.getTime(), "userRole":0
				}),
			contentType: 'application/json',
			success:function(message){
				alert("Uspesna registracija");
				window.location='./webshop.html';
			},
			error:function(message){
				alert("Greska")
			}
		});
	});
});
