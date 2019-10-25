let mojKorisnik=null;
var imgInBase64=null;
var imgInBase641=null;
var imgInBase642=null;
var imgInBase645 = null;
let allAds;
let allMsgs;
let allU;
let sveRecs;
let allCats1;

let mojIdCat;
let mojIdOgl;
let mojaPor;
let mojaRec;


//FUNKCIJA ZA PRAVLJENJE UUIDa
//*****************************
function uuidv4() {
	  return ([1e7]+-1e3+-4e3+-8e3+-1e11).replace(/[018]/g, c =>
	    (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
	  )
	}
//*******************************





//OVO CE SE IZVRSITI NA UCITAVANJU STRANICE
window.onload=function(){
	
	//DIV ZA PRIKAZ SVIH KATEGORIJA JE NA POCETKU SAKRIVENA TEK NA CLICK SE OTVARA
	$("#categoriesItems").attr("hidden",true);
	//FORMA ZA DODAVANJE KATEGORIJE JE NA POCETKU SAKRIVENA TEK NA CLICK SE OTVARA
	$('#addCategory').attr("hidden", true);
	//FORMA ZA DODAVANJE OGLASA JE NA POCETKU SAKRIVENA TEK NA KLIK SE OTVARA
	$("#addAdvertisement").attr("hidden",true);
	//DIV ZA PRIKAZ SVIH PORUKA KOJE JE POSLAO ULOGOVANI KORISNIK
	$('#myMessages').attr("hidden",true);
	//DIV ZA PRIKAZ SVIH PRIMLJENIH PORUKA ULOGOVANOG KORISNIKA
	$('#myInbox').attr("hidden",true);
	//DIV ZA FORMU ZA DODAVANJE RECENZIJE 
	$('#addRecension').attr("hidden",true);
	//DIV ZA EDIT RECENZIJE SAKRIVEN PRVO
	$('#editRecension').attr("hidden",true);
	//EDIT OGLASA HIDDEN NA POCETKU
	$('#editAd').attr("hidden",true);
	//POSTOVANE RECENZIJE
	$('#postedRecensions').attr("hidden",true);
	//EDIT KATEGORIJE
	$('#editCategory').attr("hidden",true);





	

	
	//U LOGINSERVICE JE METODA KOJA VRACA ULOGOVANOG KORISNIKA I SACUVA GA
	//U GLOBALNU PROMENLJIVU mojKorisnik
	let userRole=-1;
	$.get({
		url:'rest/currentUser',
		success: function(user){
				mojKorisnik=user;

				if(user==null){
					$('#welcome_text').text("Welcome, guest"+" ");
					$("#options_buyer").attr("hidden", true);
					$("#options_seller").attr("hidden", true);
					$("#options_admin").attr("hidden", true);
					$("#options_unregistered").attr("hidden",false);
					$('#btn_logout').attr("hidden",true);
					
					//stavi mu status pretrage na 1 da moze samo aktivne oglase da pretrazuje
					$('#statusSearch option[value=0]').attr('selected','selected');
					$('#statusSearch').attr("hidden",true);
					$('#statusLabela').attr("hidden",true);
					//$('#statusLabela').val("");

					return;
				}
				
				userRole=user.userRole;
				console.log('User je:'+user.username);
				console.log('Uloga mu je:'+user.userRole);
				$('#welcome_text').text("Welcome, "+user.username+" ");
				if(user.userRole===0){
					document.getElementById('img_icon').src="Img/userMedium.png";
					$("#options_buyer").attr("hidden", false);
					$("#options_seller").attr("hidden", true);
					$("#options_admin").attr("hidden", true);
					$("#options_unregistered").attr("hidden",true);
					$('#btn_logout').attr("hidden",false);
				}
				if(user.userRole===1){
					document.getElementById('img_icon').src="Img/adminMedium.png";
					$("#options_buyer").attr("hidden", true);
					$("#options_seller").attr("hidden", true);
					$("#options_admin").attr("hidden", false);
					$("#options_unregistered").attr("hidden",true);
					$('#btn_logout').attr("hidden",false);
				}
				if(user.userRole===2){
					document.getElementById('img_icon').src="Img/sellerMedium.png";
					$("#options_buyer").attr("hidden", true);
					$("#options_seller").attr("hidden", false);
					$("#options_admin").attr("hidden", true);
					$("#options_unregistered").attr("hidden",true);
					$('#btn_logout').attr("hidden",false);
					$('#likesDislikes').append(
							"<a style=\"background:#47748b;\" class=\"disable\">Profile likes:"+user.likesNumberSeller+"</a>"+
							"<a style=\"background:#47748b\"  class=\"disable\">Profile dislikes:"+user.dislikesNumberSeller+"</a>"
					);
				}
				
				
		}
	});
	
	
	//STAVIO TIMEOUT DA BI SIGURNO PRVO UCITAO KOJI JE TIP KORISNIKA PA TEK ONDA
	//KRENUO DA PRIKAZUJE OGLASE
	setTimeout(function(){
		//U ADVERTISEMENTSERVICE JE METODA KOJA VRACA SVE OGLASE
		//I APPENDUJE IH U SVE OGLASE I DODA IH U MULTISELECT KOD PRAVLJENJA KATEGORIJE
		$.get({
			url:'rest/ads/getAds',
			success: function(ads){
				allAds=ads;
				
				for( i=0; i<allAds.length; i++){
					console.log(allAds);
					appendAdvertisementItems(allAds[i], i);
					appendToCat(allAds[i]);
				}
			}
		});
	},100);

	setTimeout(function(){
		$.get({
			url:'rest/rec/getRecs',
			success: function(re){
				//console.log('\n\n\n'+re[0].id+'\n\n\n'+re[1].id);
				sveRecs=re;
				//console.log('\n\n\n'+sveRecs[0].id+'\n\n\n'+sveRecs[1].id);

			}
		});
	},150)
	
	//APPENDUJ GRADOVE U SELECT
	//GRADOVE UZIMA IZ GRADOVA OGLASA/GRADOVA KORISNIKA ZA PRETRAGU
	setTimeout(function(){
		appendCity();
		appendCityUserSearch();

	},500);
	
	//KLIK NA CHANGE USER ROLE DA DODA KORISNIKE KAO OPCIJE U SELECT MENI
	//KOJI CE BITI PRIKAZAN U MODALU
		$.get({
			url:'rest/getUsers',
			success: function(users){
				 allU=users;
				
				for( i=0; i<allU.length; i++){
					console.log(allU[i].username);
					console.log(allU[i]);
					appendUsers(allU[i]);
				}
			}
		});
		
		
		
		
		//DA UCITA PORUKE NA UCITAVANJU STRANICE
		$.get({
			url:'rest/message/load',
			success:function(msgs){
				console.log(msgs);
				allMsgs=msgs;
				
			}
		});
		
		
		//DA UCITA KATEGORIJE NA POCETKu
		$.get({
			url:'rest/category/getCats',
			success:function(categories){
				allCats1=categories;
				for(k=0; k<categories.length; k++){
					appendCategoriesSearch(categories[k]);
				}
			}
		});

		
		
	
};
//*******************************
//GOTOVO ONLOAD
//*******************************




//*******************************
//RAZNE FUNKCIJE
//*******************************

//PROVERA DA LI JE FOTOGRAFIJA DOBROG TIPA, AKO NIJE RESETUJ POLJE I IZBACI ALERT
function validateFileType(){
	const selectedFile = document.getElementById('ad_img').files[0];
	//console.log(selectedFile);
	
	var fileName=document.getElementById("ad_img").value;
	var idxDot = fileName.lastIndexOf(".")+1;
	var extFile = fileName.substr(idxDot, fileName.length).toLowerCase();
	if(extFile=="jpg" || extFile=="jpeg" || extFile=="png"){
		//alert("Dobra ekstenzija fajla, dobar tip fajla");
		document.getElementById("ad_path").value=fileName;
				
		var reader = new FileReader();
		reader.readAsDataURL(selectedFile);
		reader.onload = function () {
					   
			imgInBase64=reader.result;
				   
		};
		reader.onerror = function (error) {
			console.log('Error: ', error);
		};
				
				   
	}
	else{
		alert("Only jpg/jpeg and png files are allowed!");
		document.getElementById("ad_path").value='Image*';
	}
}

//PROVERA DA LI JE FOTOGRAFIJA DOBROG TIPA ZA IZMENU OGLASA
function validateFileType2(){
	const selectedFile = document.getElementById('ad_imgEdit').files[0];
	//console.log(selectedFile);
	
	var fileName=document.getElementById("ad_imgEdit").value;
	var idxDot = fileName.lastIndexOf(".")+1;
	var extFile = fileName.substr(idxDot, fileName.length).toLowerCase();
	if(extFile=="jpg" || extFile=="jpeg" || extFile=="png"){
		//alert("Dobra ekstenzija fajla, dobar tip fajla");
		document.getElementById("ad_pathEdit").value=fileName;
				
		var reader = new FileReader();
		reader.readAsDataURL(selectedFile);
		reader.onload = function () {
					   
			imgInBase642=reader.result;
			$('#slikaEditPrva').attr("src", imgInBase642);
			$('#slikaEditPrva').attr("hidden",false);
				   
		};
		reader.onerror = function (error) {
			console.log('Error: ', error);
		};
				
				   
	}
	else{
		alert("Only jpg/jpeg and png files are allowed!");
		document.getElementById("ad_pathEdit").value='Image*';
		$('#slikaEditPrva').attr("hidden",true);
	}
}

//PROVERA DA LI JE FOTOGRAFIJA DOBROG TIPA ZA RECENZIJU
function validateFileType1(){
	const selectedFile = document.getElementById('recensionImage').files[0];
	//console.log(selectedFile);
	
	var fileName=document.getElementById("recensionImage").value;
	var idxDot = fileName.lastIndexOf(".")+1;
	var extFile = fileName.substr(idxDot, fileName.length).toLowerCase();
	if(extFile=="jpg" || extFile=="jpeg" || extFile=="png"){
		//alert("Dobra ekstenzija fajla, dobar tip fajla");
		document.getElementById("recensionImg").value=fileName;
				
		var reader = new FileReader();
		reader.readAsDataURL(selectedFile);
		reader.onload = function () {
					   
			imgInBase641=reader.result;
				   
		};
		reader.onerror = function (error) {
			console.log('Error: ', error);
		};
				
				   
	}
	else{
		alert("Only jpg/jpeg and png files are allowed!");
		document.getElementById("recensionImg").value='Image*';
	}
}

//PROVERA DA LI JE FOTOGRAFIJA DOBROG TIPA ZA IZMENU RECENZIJE
function validateFileType5(){
	const selectedFile = document.getElementById('recensionImageEdit').files[0];
	//console.log(selectedFile);
	
	var fileName=document.getElementById("recensionImageEdit").value;
	var idxDot = fileName.lastIndexOf(".")+1;
	var extFile = fileName.substr(idxDot, fileName.length).toLowerCase();
	if(extFile=="jpg" || extFile=="jpeg" || extFile=="png"){
		//alert("Dobra ekstenzija fajla, dobar tip fajla");
		document.getElementById("recensionImgEdit").value=fileName;
				
		var reader = new FileReader();
		reader.readAsDataURL(selectedFile);
		reader.onload = function () {
					   
			imgInBase645=reader.result;
			$('#slikaEditDruga').attr("src", imgInBase645);
			$('#slikaEditDruga').attr("hidden",false);
				   
		};
		reader.onerror = function (error) {
			console.log('Error: ', error);
		};
				
				   
	}
	else{
		alert("Only jpg/jpeg and png files are allowed!");
		document.getElementById("recensionImgEdit").value='Image*';
	}
}


//APPEND USERA U SELECTUSER SELECT ZA CHANGE ROLE
//OVDE JE I APPEND ZA RESET WARNINGS
//RADI GA SAMO ONLOAD
//MOZE SAMO ONLOAD JER KORISNIKA NE MOZE DA DODA A DA NE UCITA STRANICU OPET
function appendUsers(u){
	
	$('#select_user').append(new Option(u.username,u.userRole));
	
	if(u.userRole==2){
		$('#select_userReset').append(new Option(u.username, u.userRole));
	}
	
}

//APPEND CATEGORY ZA SEARCH PO KATEGORIJI
function appendCategoriesSearch(c){
	if(c.active==false){
		return;
	}
	$('#catSearch').append(new Option(c.name, c.id));
}

function funkcijaZaPrikazPoKategoriji(){
	let mojIdCatZaSearch = $('#catSearch :selected').val();

	let kategorijaKojuTrazi=null;
	for(brr=0; brr<allCats1.length; brr++){
		if(allCats1[brr].id==mojIdCatZaSearch){
			kategorijaKojuTrazi=allCats1[brr];
			break;
		}
	}
	
	$('#advertisementItems').html('');
	if(kategorijaKojuTrazi.advertisements==null){
		$('#advertisementItems').html('<div class=\"col-sm-12 col-md-12 col-lg-12\"><h3>There are no ads in this category</h3></div>');
		return;
	}
	
	for(brrr=0; brrr<kategorijaKojuTrazi.advertisements.length; brrr++){
		appendAdvertisementItems(kategorijaKojuTrazi.advertisements[brrr],0);
	}
	
}


//APPEND OPCIJE GRADOVA ZA PRETRAGU OGLASA
//PONOVO SAMO ONLOAD JER NIKAD NECE DODATI OGLAS DA NE REFRESHUJE STRANICU
function appendCity(){
	let mojNiz = [];
	
	//console.log(allAds);
	
	for(q=0; q<allAds.length; q++){
		if(mojNiz.includes(allAds[q].city)){
			continue;
		}
		else{
			mojNiz.push(allAds[q].city);
			$('#gradSearch').append(new Option(allAds[q].city));
		}
	}
	

}

//APPEND OPCIJE ZA GRADOVE KORISNIKA ZA PRETRAGU KORISNIKA
//ONLOAD SAMO JER NIKAD NECE DODATI KORISNIKA DA NE RELOADUJE STRANICU
function appendCityUserSearch(){
	let mojNiz = [];
	
	for(q=0; q<allU.length; q++){
		if(mojNiz.includes(allU[q].city)){
			continue;
		}
		else{
			mojNiz.push(allU[q].city);
			$('#gradSearchUser').append(new Option(allU[q].city));
		}
	}
}



//APPEND PORUCENE PROIZVODE U DIV
//PRIKAZ SVIH PORUCENIH PROIZVODA NA CLICK NA ORDERED ITEMS
//UMESTO KORPE(BUY SLIKE) BICE SLIKA ZA POTVRDU DA JE DOSTAVLJEN PROIZVOD
//NA CLICK CE PRVO UCITATI OPET OGLASE PA IH TEK ONDA PRIKAZATI
function appendOrderedAdsBuyer(ad){
	if(ad.active==false){
		return;
	}
	if(ad.status!=1){
		return;
	}
	
	if(mojKorisnik.advertisementsFavouritesBuyer==null && ad.status==1){
		$('#advertisementItemsBuyer').append("<div class=\"col-xs-6 col-sm-4 col-md-3 col-lg-2\">"+"<div class=\"card\"  id=\""
				+ad.id+"\">" + 
				"<img class=\"card-img-top\" id=\"productImg\""+i+" src=\""+ad.imgPath+"\" alt=\"Card image cap\">" +
				"<div class=\"card-body\">" +
				"<h5 class=\"card-title\">"+ad.name+"</h5>" + 
				"<p class=\"card-text\">"+ad.description+"</p>" +
				"</div><div class=\"card-footer\">"+
				/*" <p class=\"card-text\">*/"<small class=\"text-muted\">"+
				"<table><tr><td style=\"white-space: nowrap;\">"+
				"<img src=\"Img/favMedium1.png\" class=\"adIcons\" align=\"left\"   id=\"faa"+ad.id+"\" onclick=\"changeStar1(this.id)\" ></td><td width=\"99%\">"+
				"<img src=\"Img/checked.png\" class=\"adIcons\" align=\"right\" id=\"buuu"+ad.id+"\" onclick=\"markDelivered(this.id)\"></td></tr></table>"+
				"</small>"/*</p>"*/ + 
				"</div>" +
				"</div>"+"</div>");
		return;
	}
	if(mojKorisnik.advertisementsFavouritesBuyer.includes(ad.id) && ad.status==1){
		$('#advertisementItemsBuyer').append("<div class=\"col-xs-6 col-sm-4 col-md-3 col-lg-2\">"+"<div class=\"card\"  id=\""
				+ad.id+"\">" + 
				"<img class=\"card-img-top\" id=\"productImg\""+i+" src=\""+ad.imgPath+"\" alt=\"Card image cap\">" +
				"<div class=\"card-body\">" +
				"<h5 class=\"card-title\">"+ad.name+"</h5>" + 
				"<p class=\"card-text\">"+ad.description+"</p>" +
				"</div><div class=\"card-footer\">"+
				/*" <p class=\"card-text\">*/"<small class=\"text-muted\">"+
				"<table><tr><td style=\"white-space: nowrap;\">"+
				"<img src=\"Img/favfullMedium.png\" class=\"adIcons\" align=\"left\"   id=\"faa"+ad.id+"\" onclick=\"changeStar1(this.id)\" ></td><td width=\"99%\">"+
				"<img src=\"Img/checked.png\" class=\"adIcons\" align=\"right\" id=\"buuu"+ad.id+"\" onclick=\"markDelivered(this.id)\"></td></tr></table>"+
				"</small>"/*</p>"*/ + 
				"</div>" +
				"</div>"+"</div>");
	}
	else{
		$('#advertisementItemsBuyer').append("<div class=\"col-xs-6 col-sm-4 col-md-3 col-lg-2\">"+"<div class=\"card\"  id=\""
			+ad.id+"\">" + 
			"<img class=\"card-img-top\" id=\"productImg\""+i+" src=\""+ad.imgPath+"\" alt=\"Card image cap\">" +
			"<div class=\"card-body\">" +
			"<h5 class=\"card-title\">"+ad.name+"</h5>" + 
			"<p class=\"card-text\">"+ad.description+"</p>" +
			"</div><div class=\"card-footer\">"+
			/*" <p class=\"card-text\">*/"<small class=\"text-muted\">"+
			"<table><tr><td style=\"white-space: nowrap;\">"+
			"<img src=\"Img/favMedium1.png\" class=\"adIcons\" align=\"left\"   id=\"faa"+ad.id+"\" onclick=\"changeStar1(this.id)\" ></td><td width=\"99%\">"+
			"<img src=\"Img/checked.png\" class=\"adIcons\" align=\"right\" id=\"buuu"+ad.id+"\" onclick=\"markDelivered(this.id)\"></td></tr></table>"+
			"</small>"/*</p>"*/ + 
			"</div>" +
			"</div>"+"</div>");
	}
	
}
//APPEND DELIVERED ADS BUYER ISTO KAO GORE SAMO NEMA ZVEZDICU NI CHECKED
function appendDeliveredAdsBuyer(ad){
	if(ad.active==false){
		return;
	}
		$('#advertisementItemsBuyer').append("<div class=\"col-xs-6 col-sm-4 col-md-3 col-lg-2\">"+"<div class=\"card\"  id=\""
				+ad.id+"\">" + 
				"<img class=\"card-img-top\" id=\"productImg\""+i+" src=\""+ad.imgPath+"\" alt=\"Card image cap\">" +
				"<div class=\"card-body\">" +
				"<h5 class=\"card-title\">"+ad.name+"</h5>" + 
				"<p class=\"card-text\">"+ad.description+"</p>" +
				"</div><div class=\"card-footer\">"+
				/*" <p class=\"card-text\">*/"<small class=\"text-muted\">"+
				"<table><tr><td style=\"white-space: nowrap;\">"+
				"<img src=\"Img/favMedium1.png\" class=\"adIcons\" align=\"left\"   id=\"faa"+ad.id+"\" onclick=\"changeStar1(this.id)\" ></td><td width=\"99%\">"+
				//"<img src=\"Img/checked.png\" class=\"adIcons\" align=\"right\" id=\"buuu"+ad.id+"\" onclick=\"markDelivered(this.id)\"></td></tr></table>"+
				"</small>"/*</p>"*/ + 
				"</div>" +
				"</div>"+"</div>");
}

//APPEND ZA OMILJENE OGLASE
//MORAS PRVO PROVERITI DA LI JE U OMILJENIM OGLASIMA AKO NIJE RETURN
//AKO JESTE ONDA PROVERA DA LI JE STATUS PORUCEN ILI DELIVERED
function appendFavouritesBuyer(ad){
	if(ad.active==false){
		return;
	}
	if(mojKorisnik.advertisementsFavouritesBuyer==null){
		//ako nema nista u favorites return
		return;
	}
	//ako je u listi omiljenih i ako je status u realizaciji
	if(mojKorisnik.advertisementsFavouritesBuyer.includes(ad.id) && (ad.status==1 || ad.status==0)){
		$('#advertisementItemsBuyer').append("<div class=\"col-xs-6 col-sm-4 col-md-3 col-lg-2\">"+"<div class=\"card\"  id=\""
				+ad.id+"\">" + 
				"<img class=\"card-img-top\" id=\"productImg\""+i+" src=\""+ad.imgPath+"\" alt=\"Card image cap\">" +
				"<div class=\"card-body\">" +
				"<h5 class=\"card-title\">"+ad.name+"</h5>" + 
				"<p class=\"card-text\">"+ad.description+"</p>" +
				"</div><div class=\"card-footer\">"+
				/*" <p class=\"card-text\">*/"<small class=\"text-muted\">"+
				"<table><tr><td style=\"white-space: nowrap;\">"+
				"<img src=\"Img/favfullMedium.png\" class=\"adIcons\" align=\"left\"   id=\"faa"+ad.id+"\" onclick=\"changeStar1(this.id)\" ></td><td width=\"99%\">"+
				"<img src=\"Img/checked.png\" class=\"adIcons\" align=\"right\" id=\"buuu"+ad.id+"\" onclick=\"markDelivered(this.id)\"></td></tr></table>"+
				"</small>"/*</p>"*/ + 
				"</div>" +
				"</div>"+"</div>");
	}
	
	//ako je u listi omiljenih i ako je status dostavljen
	else if(mojKorisnik.advertisementsFavouritesBuyer.includes(ad.id) && ad.status==2){
		$('#advertisementItemsBuyer').append("<div class=\"col-xs-6 col-sm-4 col-md-3 col-lg-2\">"+"<div class=\"card\"  id=\""
			+ad.id+"\">" + 
			"<img class=\"card-img-top\" id=\"productImg\""+i+" src=\""+ad.imgPath+"\" alt=\"Card image cap\">" +
			"<div class=\"card-body\">" +
			"<h5 class=\"card-title\">"+ad.name+"</h5>" + 
			"<p class=\"card-text\">"+ad.description+"</p>" +
			"</div><div class=\"card-footer\">"+
			/*" <p class=\"card-text\">*/"<small class=\"text-muted\">"+
			"<table><tr><td style=\"white-space: nowrap;\">"+
			//"<img src=\"Img/favMedium1.png\" class=\"adIcons\" align=\"left\"   id=\"faa"+ad.id+"\" onclick=\"changeStar1(this.id)\" ></td><td width=\"99%\">"+
			//"<img src=\"Img/checked.png\" class=\"adIcons\" align=\"right\" id=\"buuu"+ad.id+"\" onclick=\"markDelivered(this.id)\"></td></tr></table>"+
			"</small>"/*</p>"*/ + 
			"</div>" +
			"</div>"+"</div>");
	}
}






//PRIKAZ SVIH PROIZVODA NA UCITAVANJU STRANICE I ZA PRETRAGU
//APPEND LISTA PROIZVODA U DIV
function appendAdvertisementItems(ad, i){
	
	//AKO OGLAS NIJE AKTIVAN NECE GA PRIKAZATI
	if(ad.active==false){
		return;
	}
	
	console.log('UUID oglasa je: '+ad.id);
	//console.log('Redni broj oglasa je: '+i);
	
	
	if(mojKorisnik==null){//NEREGISTROVAN
		$('#advertisementItems').append("<div class=\"col-xs-6 col-sm-4 col-md-3 col-lg-2\">"+"<div class=\"card\"  id=\""
				+ad.id+"\">" + 
				"<img class=\"card-img-top\" id=\"productImg"+ad.id+"\" onclick=\"showProductDetails(this.id)\" src=\""+ad.imgPath+"\" alt=\"Card image cap\">" +
				"<div class=\"card-body\">" +
				"<h5 class=\"card-title\">"+ad.name+"</h5>" + 
				"<p class=\"card-text\">"+ad.description+"</p>" +
				"</div><div class=\"card-footer\">"+
				/*" <p class=\"card-text\">*/"<small class=\"text-muted\">"+
				"<table><tr><td style=\"white-space: nowrap;\">"+
				//"<img src=\"Img/favMedium1.png\" align=\"left\"   id=\"fav"+i+"\" onclick=\"changeStar(this.id)\" ></td><td width=\"99%\">"+
				//"<img src=\"Img/cartMedium.png\" align=\"right\"></td></tr></table>"+
				"</small>"/*</p>"*/ + 
				"</div>" +
				"</div>"+"</div>");
		return;
	}
	
	let flagIzPretrage=i; 
	//KAD PRETRAZUJE PORUCENE OGLASE KUPAC
	//if(mojKorisnik.userRole==0 && ad.Status==0)
	
	//ako je status oglasa iz pretrage ordered
	//ako je porucio bar neki oglas
	if(mojKorisnik.advertisementsOrderedBuyer!=null){
	if(mojKorisnik.userRole==0 && ad.status==1 && mojKorisnik.advertisementsOrderedBuyer.includes(ad.id) && flagIzPretrage==-10){
		if(mojKorisnik.advertisementsFavouritesBuyer==null){
			$('#advertisementItems').append("<div class=\"col-xs-6 col-sm-4 col-md-3 col-lg-2\">"+"<div class=\"card\"  id=\""
					+ad.id+"\">" + 
					"<img class=\"card-img-top\" id=\"productImg"+ad.id+"\" onclick=\"showProductDetails(this.id)\" src=\""+ad.imgPath+"\" alt=\"Card image cap\">" +
					"<div class=\"card-body\">" +
					"<h5 class=\"card-title\">"+ad.name+"</h5>" + 
					"<p class=\"card-text\">"+ad.description+"</p>" +
					"</div><div class=\"card-footer\">"+
					/*" <p class=\"card-text\">*/"<small class=\"text-muted\">"+
					"<table><tr><td style=\"white-space: nowrap;\">"+
					"<img src=\"Img/favMedium1.png\" class=\"adIcons\" align=\"left\"   id=\"fav"+ad.id+"\" onclick=\"changeStar(this.id)\" ></td><td width=\"99%\">"+
					"<img src=\"Img/checked.png\" class=\"adIcons\" align=\"right\" id=\"mark"+ad.id+"\" onclick=\"markDelivered(this.id)\"></td></tr></table>"+
					"</small>"/*</p>"*/ + 
					"</div>" +
					"</div>"+"</div>");
			return;
		}
		if(mojKorisnik.advertisementsFavouritesBuyer.includes(ad.id)){
			$('#advertisementItems').append("<div class=\"col-xs-6 col-sm-4 col-md-3 col-lg-2\">"+"<div class=\"card\"  id=\""
					+ad.id+"\">" + 
					"<img class=\"card-img-top\" id=\"productImg"+ad.id+"\" onclick=\"showProductDetails(this.id)\" src=\""+ad.imgPath+"\" alt=\"Card image cap\">" +
					"<div class=\"card-body\">" +
					"<h5 class=\"card-title\">"+ad.name+"</h5>" + 
					"<p class=\"card-text\">"+ad.description+"</p>" +
					"</div><div class=\"card-footer\">"+
					/*" <p class=\"card-text\">*/"<small class=\"text-muted\">"+
					"<table><tr><td style=\"white-space: nowrap;\">"+
					"<img src=\"Img/favfullMedium.png\" class=\"adIcons\" align=\"left\"   id=\"fav"+ad.id+"\" onclick=\"changeStar(this.id)\" ></td><td width=\"99%\">"+
					"<img src=\"Img/checked.png\" class=\"adIcons\" align=\"right\" id=\"mark"+ad.id+"\" onclick=\"markDelivered(this.id)\"></td></tr></table>"+
					"</small>"/*</p>"*/ + 
					"</div>" +
					"</div>"+"</div>");
		}
		else{
			$('#advertisementItems').append("<div class=\"col-xs-6 col-sm-4 col-md-3 col-lg-2\">"+"<div class=\"card\"  id=\""
				+ad.id+"\">" + 
				"<img class=\"card-img-top\" id=\"productImg"+ad.id+"\" onclick=\"showProductDetails(this.id)\" src=\""+ad.imgPath+"\" alt=\"Card image cap\">" +
				"<div class=\"card-body\">" +
				"<h5 class=\"card-title\">"+ad.name+"</h5>" + 
				"<p class=\"card-text\">"+ad.description+"</p>" +
				"</div><div class=\"card-footer\">"+
				/*" <p class=\"card-text\">*/"<small class=\"text-muted\">"+
				"<table><tr><td style=\"white-space: nowrap;\">"+
				"<img src=\"Img/favMedium1.png\" class=\"adIcons\" align=\"left\"   id=\"fav"+ad.id+"\" onclick=\"changeStar(this.id)\" ></td><td width=\"99%\">"+
				"<img src=\"Img/checked.png\" class=\"adIcons\" align=\"right\" id=\"mark"+ad.id+"\" onclick=\"markDelivered(this.id)\"></td></tr></table>"+
				"</small>"/*</p>"*/ + 
				"</div>" +
				"</div>"+"</div>");
		}
		
		return;
	}
	}
	
	//ako je status oglasa iz pretrage delivered, nece prikazati ordered oglase
	//ako mu je stigao bar neki oglas
	if(mojKorisnik.advertisementsDeliveredBuyer!=null){
	if(mojKorisnik.userRole==0 && ad.status==2 && mojKorisnik.advertisementsDeliveredBuyer.includes(ad.id) && flagIzPretrage==-101){
		if(mojKorisnik.advertisementsFavouritesBuyer==null){
			$('#advertisementItems').append("<div class=\"col-xs-6 col-sm-4 col-md-3 col-lg-2\">"+"<div class=\"card\"  id=\""
					+ad.id+"\">" + 
					"<img class=\"card-img-top\" id=\"productImg"+ad.id+"\" onclick=\"showProductDetails(this.id)\" src=\""+ad.imgPath+"\" alt=\"Card image cap\">" +
					"<div class=\"card-body\">" +
					"<h5 class=\"card-title\">"+ad.name+"</h5>" + 
					"<p class=\"card-text\">"+ad.description+"</p>" +
					"</div><div class=\"card-footer\">"+
					/*" <p class=\"card-text\">*/"<small class=\"text-muted\">"+
					"<table><tr><td style=\"white-space: nowrap;\">"+
					"<img src=\"Img/favMedium1.png\" class=\"adIcons\" align=\"left\"   id=\"fav"+ad.id+"\" onclick=\"changeStar(this.id)\" ></td><td width=\"99%\">"+
					//"<img src=\"Img/checked.png\" class=\"adIcons\" align=\"right\" id=\"buy"+ad.id+"\" onclick=\"markDelivered(this.id)\"></td></tr></table>"+
					"</small>"/*</p>"*/ + 
					"</div>" +
					"</div>"+"</div>");
			return;
		}
		if(mojKorisnik.advertisementsFavouritesBuyer.includes(ad.id)){
			$('#advertisementItems').append("<div class=\"col-xs-6 col-sm-4 col-md-3 col-lg-2\">"+"<div class=\"card\"  id=\""
					+ad.id+"\">" + 
					"<img class=\"card-img-top\" id=\"productImg"+ad.id+"\" onclick=\"showProductDetails(this.id)\" src=\""+ad.imgPath+"\" alt=\"Card image cap\">" +
					"<div class=\"card-body\">" +
					"<h5 class=\"card-title\">"+ad.name+"</h5>" + 
					"<p class=\"card-text\">"+ad.description+"</p>" +
					"</div><div class=\"card-footer\">"+
					/*" <p class=\"card-text\">*/"<small class=\"text-muted\">"+
					"<table><tr><td style=\"white-space: nowrap;\">"+
					"<img src=\"Img/favfullMedium.png\" class=\"adIcons\" align=\"left\"   id=\"fav"+ad.id+"\" onclick=\"changeStar(this.id)\" ></td><td width=\"99%\">"+
					//"<img src=\"Img/cartMedium.png\" class=\"adIcons\" align=\"right\" id=\"buy"+ad.id+"\" onclick=\"buyAd(this.id)\"></td></tr></table>"+
					"</small>"/*</p>"*/ + 
					"</div>" +
					"</div>"+"</div>");
		}
		else{
			$('#advertisementItems').append("<div class=\"col-xs-6 col-sm-4 col-md-3 col-lg-2\">"+"<div class=\"card\"  id=\""
				+ad.id+"\">" + 
				"<img class=\"card-img-top\" id=\"productImg"+ad.id+"\" onclick=\"showProductDetails(this.id)\" src=\""+ad.imgPath+"\" alt=\"Card image cap\">" +
				"<div class=\"card-body\">" +
				"<h5 class=\"card-title\">"+ad.name+"</h5>" + 
				"<p class=\"card-text\">"+ad.description+"</p>" +
				"</div><div class=\"card-footer\">"+
				/*" <p class=\"card-text\">*/"<small class=\"text-muted\">"+
				"<table><tr><td style=\"white-space: nowrap;\">"+
				"<img src=\"Img/favMedium1.png\" class=\"adIcons\" align=\"left\"   id=\"fav"+ad.id+"\" onclick=\"changeStar(this.id)\" ></td><td width=\"99%\">"+
				//"<img src=\"Img/cartMedium.png\" class=\"adIcons\" align=\"right\" id=\"buy"+ad.id+"\" onclick=\"buyAd(this.id)\"></td></tr></table>"+
				"</small>"/*</p>"*/ + 
				"</div>" +
				"</div>"+"</div>");
		}
		
		return;
	}
	}
	
	
	if(mojKorisnik.userRole==0 && ad.status!=1 && ad.status!=2){ //KUPAC
		if(mojKorisnik.advertisementsFavouritesBuyer==null){
			$('#advertisementItems').append("<div class=\"col-xs-6 col-sm-4 col-md-3 col-lg-2\">"+"<div class=\"card\"  id=\""
					+ad.id+"\">" + 
					"<img class=\"card-img-top\" id=\"productImg"+ad.id+"\" onclick=\"showProductDetails(this.id)\" src=\""+ad.imgPath+"\" alt=\"Card image cap\">" +
					"<div class=\"card-body\">" +
					"<h5 class=\"card-title\">"+ad.name+"</h5>" + 
					"<p class=\"card-text\">"+ad.description+"</p>" +
					"</div><div class=\"card-footer\">"+
					/*" <p class=\"card-text\">*/"<small class=\"text-muted\">"+
					"<table><tr><td style=\"white-space: nowrap;\">"+
					"<img src=\"Img/favMedium1.png\" class=\"adIcons\" align=\"left\"   id=\"fav"+ad.id+"\" onclick=\"changeStar(this.id)\" ></td><td width=\"99%\">"+
					"<img src=\"Img/cartMedium.png\" class=\"adIcons\" align=\"right\" id=\"buy"+ad.id+"\" onclick=\"buyAd(this.id)\"></td></tr></table>"+
					"</small>"/*</p>"*/ + 
					"</div>" +
					"</div>"+"</div>");
			return;
		}
		if(mojKorisnik.advertisementsFavouritesBuyer.includes(ad.id)){
			$('#advertisementItems').append("<div class=\"col-xs-6 col-sm-4 col-md-3 col-lg-2\">"+"<div class=\"card\"  id=\""
					+ad.id+"\">" + 
					"<img class=\"card-img-top\" id=\"productImg"+ad.id+"\" onclick=\"showProductDetails(this.id)\"src=\""+ad.imgPath+"\" alt=\"Card image cap\">" +
					"<div class=\"card-body\">" +
					"<h5 class=\"card-title\">"+ad.name+"</h5>" + 
					"<p class=\"card-text\">"+ad.description+"</p>" +
					"</div><div class=\"card-footer\">"+
					/*" <p class=\"card-text\">*/"<small class=\"text-muted\">"+
					"<table><tr><td style=\"white-space: nowrap;\">"+
					"<img src=\"Img/favfullMedium.png\" class=\"adIcons\" align=\"left\"   id=\"fav"+ad.id+"\" onclick=\"changeStar(this.id)\" ></td><td width=\"99%\">"+
					"<img src=\"Img/cartMedium.png\" class=\"adIcons\" align=\"right\" id=\"buy"+ad.id+"\" onclick=\"buyAd(this.id)\"></td></tr></table>"+
					"</small>"/*</p>"*/ + 
					"</div>" +
					"</div>"+"</div>");
		}
		else{
			$('#advertisementItems').append("<div class=\"col-xs-6 col-sm-4 col-md-3 col-lg-2\">"+"<div class=\"card\"  id=\""
				+ad.id+"\">" + 
				"<img class=\"card-img-top\" id=\"productImg"+ad.id+"\" onclick=\"showProductDetails(this.id)\" src=\""+ad.imgPath+"\" alt=\"Card image cap\">" +
				"<div class=\"card-body\">" +
				"<h5 class=\"card-title\">"+ad.name+"</h5>" + 
				"<p class=\"card-text\">"+ad.description+"</p>" +
				"</div><div class=\"card-footer\">"+
				/*" <p class=\"card-text\">*/"<small class=\"text-muted\">"+
				"<table><tr><td style=\"white-space: nowrap;\">"+
				"<img src=\"Img/favMedium1.png\" class=\"adIcons\" align=\"left\"   id=\"fav"+ad.id+"\" onclick=\"changeStar(this.id)\" ></td><td width=\"99%\">"+
				"<img src=\"Img/cartMedium.png\" class=\"adIcons\" align=\"right\" id=\"buy"+ad.id+"\" onclick=\"buyAd(this.id)\"></td></tr></table>"+
				"</small>"/*</p>"*/ + 
				"</div>" +
				"</div>"+"</div>");
		}
	}
	
	if(mojKorisnik.userRole==1){ //ADMIN
		$('#advertisementItems').append("<div class=\"col-xs-6 col-sm-4 col-md-3 col-lg-2\">"+"<div class=\"card\"  id=\""
				+ad.id+"\">" + 
				"<img class=\"card-img-top\" id=\"productImg"+ad.id+"\" onclick=\"showProductDetails(this.id)\" src=\""+ad.imgPath+"\" alt=\"Card image cap\">" +
				"<div class=\"card-body\">" +
				"<h5 class=\"card-title\">"+ad.name+"</h5>" + 
				"<p class=\"card-text\">"+ad.description+"</p>" +
				"</div><div class=\"card-footer\">"+
				/*" <p class=\"card-text\">*/"<small class=\"text-muted\">"+
				"<table><tr><td style=\"white-space: nowrap;\">"+
				"<img src=\"Img/penMedium.png\" class=\"adIcons\" align=\"left\"   id=\"pen"+ad.id+"\" onclick=\"changeAd(this.id)\"  ></td><td width=\"99%\">"+
				"<img src=\"Img/trash1Medium.png\" class=\"adIcons\" id=\"admin_remove"+ad.id+"\" onclick=\"adminRemoveAdFunction(this.id) \"align=\"right\"></td></tr></table>"+
				"</small>"/*</p>"*/ + 
				"</div>" +
				"</div>"+"</div>");
	}
	
	if(mojKorisnik.userRole==2){ //SELLER
		if(mojKorisnik.advertisementsPostedSeller!=null){
		for( j=0; j<mojKorisnik.advertisementsPostedSeller.length; j++){
			if(mojKorisnik.advertisementsPostedSeller[j]==ad.id){
				$('#advertisementItems').append("<div class=\"col-xs-6 col-sm-4 col-md-3 col-lg-2\">"+"<div class=\"card\"  id=\""
						+ad.id+"\">" + 
						"<img class=\"card-img-top\" id=\"productImg"+ad.id+"\" onclick=\"showProductDetails(this.id)\" src=\""+ad.imgPath+"\" alt=\"Card image cap\">" +
						"<div class=\"card-body\">" +
						"<h5 class=\"card-title\">"+ad.name+"</h5>" + 
						"<p class=\"card-text\">"+ad.description+"</p>" +
						"</div><div class=\"card-footer\">"+
						/*" <p class=\"card-text\">*/"<small class=\"text-muted\">"+
						"<table><tr><td style=\"white-space: nowrap;\">"+
						"<img src=\"Img/penMedium.png\" class=\"adIcons\" align=\"left\"   id=\"pen"+ad.id+"\" onclick=\"changeAd(this.id)\"  ></td><td width=\"99%\">"+
						"<img src=\"Img/trash1Medium.png\" class=\"adIcons\" id=\"sellr_remove"+ad.id +"\" onclick=\"adminRemoveAdFunction(this.id)\" align=\"right\"></td></tr></table>"+
						"</small>"/*</p>"*/ + 
						"</div>" +
						"</div>"+"</div>");
				
				return;
			}
		}
		}

		$('#advertisementItems').append("<div class=\"col-xs-6 col-sm-4 col-md-3 col-lg-2\">"+"<div class=\"card\"  id=\""
				+ad.id+"\">" + 
				"<img class=\"card-img-top\" id=\"productImg"+ad.id+"\" onclick=showProductDetails(this.id) src=\""+ad.imgPath+"\" alt=\"Card image cap\">" +
				"<div class=\"card-body\">" +
				"<h5 class=\"card-title\">"+ad.name+"</h5>" + 
				"<p class=\"card-text\">"+ad.description+"</p>" +
				"</div><div class=\"card-footer\">"+
				/*" <p class=\"card-text\">*/"<small class=\"text-muted\">"+
				"<table><tr><td style=\"white-space: nowrap;\">"+
				"<img src=\"Img/favMedium1.png\" class=\"adIcons\" align=\"left\"   id=\"fav"+ad.id+"\" onclick=\"changeStar(this.id)\" ></td><td width=\"99%\">"+
				"<img src=\"Img/cartMedium.png\" class=\"adIcons\" align=\"right\" id=\"buy"+ad.id+"\" onclick=\"buyAd(this.id)\"></td></tr></table>"+
				"</small>"/*</p>"*/ + 
				"</div>" +
				"</div>"+"</div>");
	}
	
	
}

//CHANGE AD
//CLICK NA OLOVKU IZBACI FORMU ZA CHANGE AD
function changeAd(x){
	let mojString50 = x.substr(3);
	
	$('#myMessages').attr("hidden",true);
	$('#myInbox').attr("hidden",true);
	$("#categoriesItems").attr("hidden",true);
	$('#addCategory').attr("hidden", true);
	$("#addAdvertisement").attr("hidden",true);
	$("#advertisementItems").attr("hidden",true);
	$("#advertisementItemsBuyer").attr("hidden",true);
	$('#postedRecensions').attr("hidden",true);
	$('#addRecension').attr("hidden",true);
	$('#editRecension').attr("hidden",true);

	$('#editAd').attr("hidden",false);

	//alert('udje')
	let oglasIzmena = null;
	
	for(q=0; q<allAds.length; q++){
		if(allAds[q].id==mojString50){
			oglasIzmena=allAds[q];
			mojIdOgl=oglasIzmena.id;
		}
	}
	
	if(oglasIzmena==null){
		alert('Nije pronadjen');
		return;
	}
	
	$('#ad_nameEdit').val(oglasIzmena.name);
	$('#ad_priceEdit').val(oglasIzmena.price);
	$('#ad_descriptionEdit').val(oglasIzmena.description);
	
	$('#slikaEditPrva').attr('src', oglasIzmena.imgPath);
	
	$('#ad_cityEdit').val(oglasIzmena.city);
	
}

//SHOW PRODUCT DETAILS KROZ MODAL
//BILO GDE DA KLIKNE NA SLIKU PROIZVODA PRIKAZE MU DETALJE
function showProductDetails(x){
	
	let mojString10 = x.substr(10);
	let mojOglas10;
	
	for(q=0; q<allAds.length; q++){
		if(allAds[q].id==mojString10){
			mojOglas10=allAds[q];
		}
	}
	
	let datum10 = new Date(mojOglas10.datePublished);
	let datum100 = new Date(mojOglas10.dateExpired);

	
	$('#modalDetalji').modal('toggle');
	
	$('#teloModalDetalji').html('');
	$('#teloModalDetalji').append(
			
			"<div class=\"card\" style=\"width:95%;\">"+
			  "<img src=\""+mojOglas10.imgPath+"\" class=\"card-img-top\" alt=\"...\">"+
			  "<div class=\"card-body\">"+
			    "<h5 class=\"card-title\">"+mojOglas10.name+"</h5>"+
			    "<p class=\"card-text\">"+
			    	"<p>Price: "+mojOglas10.price + "</p>"+
			    	"<p>Description: "+mojOglas10.description + "</p>"+
			    	"<p>Likes: "+mojOglas10.numLikes +" Dislikes: "+mojOglas10.numDislikes + "</p>"+
			    	"<p>City: "+mojOglas10.city + "</p>"+
			    	"<p>Datum postavljanja: "+datum10.getDate()+"/"+datum10.getMonth()+"/"+datum10.getYear() + "</p>"+
			    	"<p>Datum isticanja: "+datum100.getDate()+"/"+datum100.getMonth()+"/"+datum100.getYear() + "</p>"+
			    	"<p>Status: "+mojOglas10.status + "</p>"+

			    "</p>"+
			  "</div>"+
			"</div>"
			
	);
}

//APPEND CATEGORIES
//PRIKAZ KATEGORIJA
function appendCategories(cat){
	
	
	if(cat.active==true){
	$('#categoriesItems').append(
			"<div class=\"card\" style=\"width: 18rem;\">"+
			  "<div class=\"card-body\">"+
			    "<h5 class=\"card-title\">"+cat.name+"</h5>"+
			    "<p class=\"card-text\">"+cat.description+"</p>"+
			    "<a href=\"#\" id=\"izmeni"+cat.id+"\" class=\"card-link\" onclick=\"editCat(this.id)\">Izmeni</a>"+
			    "<a href=\"#\" id=\"obrisi"+cat.id+"\" class=\"card-link\" onclick=\"deleteCat(this.id); return false;\">Obrisi</a>"+
			 " </div>"+
			"</div>"	
	);
	}
	else{
		return;
	}
}

//KLIK NA EDIT KATEGORIJU FUNKCIJA
function editCat(x){
	let mojString11 = x.substr(6);
	$('#myMessages').attr("hidden",true);
	$('#myInbox').attr("hidden",true);
	$("#categoriesItems").attr("hidden",true);
	$('#addCategory').attr("hidden", true);
	$("#addAdvertisement").attr("hidden",true);
	$("#advertisementItems").attr("hidden",true);
	$("#advertisementItemsBuyer").attr("hidden",true);
	$('#addRecension').attr("hidden",true);
	$('#editAd').attr("hidden",true);
	$('#postedRecensions').attr("hidden",true);
	$('#editCategory').attr("hidden",false);
	$('#editRecension').attr("hidden",true);
	

	
	console.log(mojString11);
	mojIdCat=mojString11;
	let mojaKategorija;
	for(g=0; g<allCats1.length; g++){
		if(allCats1[g].id==mojString11){
			mojaKategorija=allCats1[g];
			
			break;
		}
		//console.log(allCats1[1]);

	}
	//console.log("\n\n\n\n"+mojaKategorija);
	//alert('udjem');

	$('#cat_nameEdit').val(mojaKategorija.name);
	$('#cat_descriptionEdit').val(mojaKategorija.description);
	
	
	let mojNizIdAd=[];
	for(r=0; r<mojaKategorija.advertisements.length; r++){
		mojNizIdAd.push(mojaKategorija.advertisements[r].id);
	}
	
	console.log(mojaKategorija.advertisements);
    //$("#groupsel_0 option[value=105]").prop("selected", true); 
    
    $("#cat_selectEdit > option").each(function() {
        //alert(this.text + ' ' + this.value);
    	if(mojNizIdAd.includes(this.value)){
    	    $("#cat_selectEdit option[value="+this.value+"]").prop("selected", true); 

    	}
    });

    
    
}


//PRIKAZI RECENZIJE POSLATE OD KORISNIKA
function appendRecs(r){
	let prom = "Neodgovarajuci opis";
	let prom1="Neispunjen dogovor";
	
	if(r.adDescriptionCorrect){
		prom="Odgovarajuci opis";
	}
	
	if(r.dealFulfilled){
		prom1="Ispunjen dogovor";
	}
	
	if(r.title!="obrisanaRecenzija" && mojKorisnik.userRole==0){
		//alert('EVO ME');
		$('#postedRecensions').append(
				"<div class=\"card\" style=\"margin:10px\">"+
				  "<div class=\"card-body\">"+
				    "<h5 class=\"card-title\">"+r.title+"</h5>"+
				    "<p class=\"card-text\">"+
				    "<p>"+r.content+"</p>"+
				    "<p>"+prom+"</p>"+
				    "<p>"+prom1+"</p>"+
				    "</p>"+
				    "<a href=\"#\" id=\"izmeni"+r.id+"\" class=\"card-link\" onclick=\"editRec(this.id)\">Izmeni</a>"+
				    "<a href=\"#\" id=\"obrisi"+r.id+"\" class=\"card-link\" onclick=\"deleteRec(this.id); return false;\">Obrisi</a>"+
				 " </div>"+
				"</div>"
		);
	}
	if(r.title!="obrisanaRecenzija" && mojKorisnik.userRole==2 && r.imgPath!="NemaSliku"){
		alert('EVO ME');
		$('#postedRecensions').append(
				"<div class=\"card\" style=\"margin:10px\">"+
				  "<div class=\"card-body\">"+
				    "<h5 class=\"card-title\">"+r.title+"</h5>"+
				    "<p class=\"card-text\">"+
				    "<p>"+r.content+"</p>"+
				    "<p>"+prom+"</p>"+
				    "<p>"+prom1+"</p>"+
				    "<img src=\""+r.imgPath+"\">"+
				    "</p>"+
				 " </div>"+
				"</div>"
		);
	}
	if(r.title!="obrisanaRecenzija" && mojKorisnik.userRole==2 && r.imgPath=="NemaSliku"){
		alert('EVO ME');
		$('#postedRecensions').append(
				"<div class=\"card\" style=\"margin:10px\">"+
				  "<div class=\"card-body\">"+
				    "<h5 class=\"card-title\">"+r.title+"</h5>"+
				    "<p class=\"card-text\">"+
				    "<p>"+r.content+"</p>"+
				    "<p>"+prom+"</p>"+
				    "<p>"+prom1+"</p>"+
				    "</p>"+
				 " </div>"+
				"</div>"
		);
	}
}

//CLICK NA EDITREC DA PRIKAZE FORMU ZA IZMENU RECENZIJE
function editRec(x){
	let mojString101 = x.substr(6);
	
	$('#myMessages').attr("hidden",true);
	$('#myInbox').attr("hidden",true);
	$("#categoriesItems").attr("hidden",true);
	$('#addCategory').attr("hidden", true);
	$("#addAdvertisement").attr("hidden",true);
	$("#advertisementItems").attr("hidden",true);
	$("#advertisementItemsBuyer").attr("hidden",true);
	$('#postedRecensions').attr("hidden",true);
	$('#addRecension').attr("hidden",true);
	$('#editRecension').attr("hidden",false);
	$('#editAd').attr("hidden",true);
	
	let test5=null;
	mojaRec=null;
	for(q=0; q<sveRecs.length; q++){
		if(sveRecs[q].id==mojString101){
			mojaRec=sveRecs[q];
			//console.log(sveRecs[q]);
			test5=sveRecs[q];
			break;
		}
	}
	//console.log(mojaRec);
	console.log(sveRecs);
	if(test5==null){
		alert('Review not found');
		return;
	}
	//console.log(mojaRec.imgPath);
	$('#adNameRecensionEdit').val(mojaRec.ad);
	$('#titleRecensionEdit').val(mojaRec.title);
	$('#descriptionRecensionEdit').val(mojaRec.content);
	if(mojaRec.imgPath=="NemaSliku"){
		$('#slikaEditDruga').attr("hidden",true);
	}
	else{
		$('#slikaEditDruga').attr("src", mojaRec.imgPath);
		$('#slikaEditDruga').attr("hidden",false);
	}
	if(mojaRec.adDescriptionCorrect){
		$('#recensionDescriptionCbEdit').prop("checked",true);
	}
	else{
		$('#recensionDescriptionCbEdit').prop("checked",false);
	}
	if(mojaRec.dealFulfilled){
		$('#recensionDealCbEdit').prop("checked",true);
	}
	else{
		$('#recensionDealCbEdit').prop("checked",false);
	}
}


//NA CLICK DUGMETA LOGICKI BRISE RECENZIJU
function deleteRec(x){
	let mojString9 = x.substr(6);
	$.post({
		url:'rest/rec/deleteRec/'+mojString9,
		success:function(){
			location.hash="mojHashDeleteRec";
			location.reload();
			//$("postedReviews").trigger("click");
		},
		error:function(){
			alert('Neuspelo brisanje');
		}
	});
}

//NA CLICK DUGMETA LOGICKI BRISE KATEGORIJU
function deleteCat(x){
	
	let mojaCat = x.substr(6);
	$.post({
		url:'rest/category/deleteCat/'+mojaCat,
		success: function(){
			//alert('Uspesno brisanje kategorije');
			//window.location.reload();
			$("#show_categories").trigger("click");
		},
		error: function(){
			alert('Neuspesno brisanje kategorije');
		}
	});
}


//APPEND TO CATEGORY, DODAJ OGLASE KATEGORIJI, KAD PRAVI NOVI OGLAS
//U MULTI SELECTU SE POJAVLJUJU SVI OGLASI KAO OPCIJE
function appendToCat(a){
	$('#cat_select').append(new Option(a.name, a.id));
	$('#cat_selectEdit').append(new Option(a.name, a.id));

}



//APPEND AD RECENSION
function appendAdRecension(){
	
	
	
	//dodace samo one oglase za koje jos nije postavljena recenzija
	for(q=0; q<allAds.length; q++){
		let mozeDodaj=1;

		for(y=0; y<sveRecs.length; y++){
			if(allAds[q].recensions!=null){
				if(allAds[q].recensions.includes(sveRecs[y].id)){
					mozeDodaj=0;
					break;
				}
			}
		}
		if(mojKorisnik.advertisementsDeliveredBuyer!=null && mojKorisnik.advertisementsDeliveredBuyer!="" && mojKorisnik.advertisementsDeliveredBuyer!=undefined){
		if(mojKorisnik.advertisementsDeliveredBuyer.includes(allAds[q].id) && mozeDodaj==1){
			$('#adNameRecension').append(new Option(allAds[q].name, allAds[q].id));
		}
		}
	}
}

//APPEND USERS MESSAGE
//KAD KREIRA PORUKU DA PONUDI KOJIM KORISNICIMA SME IZ SELECTA
function appendUsersMsg(u){

	$('#select_receiver').append(new Option(u.username, u.username));
}
//APPEND ADS MESSAGE
function appendAdsMsg(){
	$('#select_ad_compose').html('');
	$('#select_ad_compose').append("<option value=\"\" disabled selected hidden=\"true\">Please select ad...</option>");
	
	
	
	for(l=0; l<allAds.length; l++){
		$('#select_ad_compose').append(new Option(allAds[l].name, allAds[l].id));
		
	}
}

function appendAdsEditMsg(){
	$('#select_ad_edit').html('');
	
	for(l=0; l<allAds.length; l++){
		$('#select_ad_edit').append(new Option(allAds[l].name, allAds[l].id));
	}
}

//APPEND ADS U SELECT KAD PRODAVAC ODGOVARA KUPCU NA PORUKU
function appendAdsMsgReply(){
	$('#select_ad_reply').html('');
	$('#select_ad_reply').append("<option value=\"\" disabled selected hidden=\"true\">Please select ad...</option>");
	
	for(l=0; l<allAds.length; l++){
		$('#select_ad_reply').append(new Option(allAds[l].name, allAds[l].id));
	}
}

//APPEND SVIH POSLATIH PORUKA 
function appendMyMessages(m){
	if(m.deleted==true){
		return;
	}
	var mojDatum = new Date(m.dateAndTime);
	
	$('#myMessages').append(
			"<div class=\"card\" id=\"msgCard"+m.id+"\" style=\"margin:10px\">"+
			  "<div class=\"card-header\">Receiver: "+
			    m.receiver+
			  "</div>"+
			  "<div class=\"card-body\">"+
			    "<h5 class=\"card-title\">"+m.msgTitle+"</h5>"+
			    "<p class=\"card-text\">"+m.msgContent+"</p>"+
			    "<p align=\"right\">"+
			    "<a href=\"#\" id=\"deleteMsg"+m.id+"\" onclick=\"deleteMsgFun(this.id)\">Obrisi</a>"+
			    "  "+
			    "<a href=\"#\" id=\"editMsg"+m.id+"\" onclick=\"editMsgFun(this.id)\" >Izmeni</a>"+
			    "</p>"+
			 "</div>"+
			 "<div class=\"card-footer text-muted\">"+
		      mojDatum.getHours()+":"+mojDatum.getMinutes()+"  "+mojDatum.getDate()+"/"+mojDatum.getMonth()+"/"+mojDatum.getFullYear()+
		     "</div>"+
			"</div>"
	);
}

//DA SE POJAVI MODAL ZA EDIT MESSAGE
function editMsgFun(x){
	let mojString3 = x.substr(7);
	
	let mojaPoruka=null;
	//console.log(mojString3);
	for(g=0; g<allMsgs.length; g++){
		//alert(allMsgs[g].id);
		if(allMsgs[g].id==mojString3){
			mojaPoruka=allMsgs[g];
			mojaPor = allMsgs[g];
		}
	}
	
	if(mojaPoruka==null){
		alert('Nije nasao id poruke');
		return;
	}
	
	$('#edit_message_modal').modal('toggle');
	
	appendAdsEditMsg();
	
	$("#select_ad_edit > option").each(function() {
        //alert(this.text + ' ' + this.value);
    	if(mojaPoruka.ad==(this.text)){
    	    $("#select_ad_edit option[value="+this.value+"]").prop("selected", true); 

    	}
    });
	
	$('#message_title_edit').val(mojaPoruka.msgTitle);
	$('#message_description_edit').val(mojaPoruka.msgContent);

}

//DA PRODAVAC MOZE DA ODGOVORI KORISNIKU NA PORUKU
function replyToBuyer(x){
	let mojString3 = x.substr(12);
	let mojaPoruka=null;
	for(g=0; g<allMsgs.length; g++){
		if(allMsgs[g].id==mojString3){
			mojaPoruka=allMsgs[g];
		}
	}
	
	if(mojaPoruka!=null){
		$('#reply_receiver').val(mojaPoruka.sender);
		$('#reply_title').val(mojaPoruka.msgTitle);
	}
	
	appendAdsMsgReply();
}

//APPEND PORUKA U INBOX
function appendMyInbox(m){
	if(m.deleted==true){
		return;
	}
	var mojDatum = new Date(m.dateAndTime);
	
	let sviKupci=[];
	
	for(e=0; e<allU.length; e++){
		if(allU[e].userRole==0){
			sviKupci.push(allU[e].username);
		}
	}
	
	if(mojKorisnik.userRole==2 && sviKupci.includes(m.sender)){
		$('#myInbox').append(
				"<div class=\"card\" id=\"msgCardInbox"+m.id+"\" style=\"margin:10px\">"+
				  "<div class=\"card-header\">Sender: "+
				    m.sender+
				  "</div>"+
				  "<div class=\"card-body\">"+
				    "<h5 class=\"card-title\">"+m.msgTitle+"</h5>"+
				    "<p class=\"card-text\">"+m.msgContent+"</p>"+
				    "<p align=\"right\">"+
				   "<a href=\"#\" id=\"replyToBuyer" +m.id+"\" onclick=\"replyToBuyer(this.id)\""+
				   "data-toggle=\"modal\" data-target=\"#reply_message_modal\""+
				   ">Reply</a>"+
				    //"  "+
				    //"<a href=\"#\" >Izmeni</a>"+
				    "</p>"+
				 "</div>"+
				 "<div class=\"card-footer text-muted\">"+
			      mojDatum.getHours()+":"+mojDatum.getMinutes()+"  "+mojDatum.getDate()+"/"+mojDatum.getMonth()+"/"+mojDatum.getFullYear()+
			     "</div>"+
				"</div>"
		);
	}
	else{
		$('#myInbox').append(
				"<div class=\"card\" id=\"msgCardInbox"+m.id+"\" style=\"margin:10px\">"+
				  "<div class=\"card-header\">Sender: "+
				    m.sender+
				  "</div>"+
				  "<div class=\"card-body\">"+
				    "<h5 class=\"card-title\">"+m.msgTitle+"</h5>"+
				    "<p class=\"card-text\">"+m.msgContent+"</p>"+
				    //"<p align=\"right\">"+
				   // "<a href=\"#\" id=\"deleteMsgInbox"+m.id+"\" onclick=\"deleteMsgFun(this.id)\">Obrisi</a>"+
				    //"  "+
				    //"<a href=\"#\" >Izmeni</a>"+
				    //"</p>"+
				 "</div>"+
				 "<div class=\"card-footer text-muted\">"+
			      mojDatum.getHours()+":"+mojDatum.getMinutes()+"  "+mojDatum.getDate()+"/"+mojDatum.getMonth()+"/"+mojDatum.getFullYear()+
			     "</div>"+
				"</div>"
		);
	}
}


function deleteMsgFun(x){
	let mojString = x.substr(9);
	
	console.log(mojString);
	let mojString2 = "msgCard"+mojString;
	
	
	$.get({
		url:'rest/message/delete/'+mojString,
		success: function(){
			$('#'+mojString2).attr("hidden",true);
			
			
			//OVDE IDE KROZ SVU DECU DIVA GDE SU PORUKE
			//AKO SU SVI HIDDEN ZNACI DA SU SVI OBRISANI I TREBA DA PRIKAZE DA 
			//NEMA VISE PORUKA
			var nemaPor=0;
			
			$('#myMessages').children().each(function(){
				var el = document.getElementById(this.id);
				//console.log(el.getAttribute("hidden"));
				if(el.getAttribute("hidden")==null){
					nemaPor=1;
				}
			});
			
			if(nemaPor==0){
				$("#myMessages").append(
						"<h3 align=\"center\">Outbox is empty</h3>"
				);
			}

		},
		error:function(){
			alert('Greska');
		}
	});
}

//FUNKCIJA KOJA BRISE LOGICKI OGLAS OD STRANE ADMINA
function adminRemoveAdFunction(x){
	let brOglasa = x.substring(12);
	alert(brOglasa);
	
	let mojOglas;
	
	for(q=0; q<allAds.length; q++){
		if(brOglasa==allAds[q].id){
			mojOglas = allAds[q];
		}
	}
	
	if(mojOglas.status == 1){
		alert('Oglas koji ima status u realizaciji nije moguce obrisati');
		return;
	}
	if(mojOglas.status == 2){
		alert('Oglas koji ima status dostavljen nije moguce obrisati');
		return;
	}
	
	$.get({
		url:'rest/ads/delete/'+brOglasa,
		contentType: 'application/json',
		success:function(){
			window.location.reload();
			//window.location='./webshop.html';
		},
		error:function(){
			alert('Error: unable to delete ad');
		}
	});
}


//FUNCKIJA KOJA KUPUJE OGLAS OD STRANE KUPCA
function buyAd(x){
	let mojString4=x.substr(3);
	$.get({
		url:'rest/ads/buy/'+mojString4,
		success:function(){
			alert("Advertisement ordered");
			window.location.reload();
		},
		error:function(){
			alert("Error");
		}
	});
}

//FUNKCIJA KOJA OZNACAVA DA JE OGLAS DOSTAVLJEN
function markDelivered(x){
	let mojString101=x.substr(4);
	$.post({
		url:'rest/ads/markD/'+mojString101,
		success:function(){
			alert('Ad marked as delivered');
			window.location.reload();
		},
		error:function(){
			alert('Error');
		}
	})
}

//FUNKCIJA KOJA MENJA IKONICU I DODAJE/BRISE OGLAS IZ FAVORITE OGLASA KUPCA
function changeStar(x){
	
	if($("#"+x).attr("src")==="Img/favMedium1.png"){
		$("#"+x).attr("src", "Img/favfullMedium.png");
		var idOglasa= x.substr(3);
		//console.log("MOJ ID JE:"+idOglasa);
		$.post({
			url:'rest/addToFav/'+idOglasa,
			success:function(){
				alert("Added to favourites");
				//da li treba ovaj refresh
				window.location='./webshop.html'
			},
			error:function(){
				alert('Error occured');
			}
		});
	}
	else{
		$("#"+x).attr("src", "Img/favMedium1.png");
		var idOglasa= x.substr(3);
		$.post({
			url:'rest/removeFromFav/'+idOglasa,
			success:function(){
				alert("Deleted from favourites");
				//da li treba ovaj refresh
				window.location='./webshop.html'
			},
			error:function(){
				alert('Error occured');
			}
		});
	}
}

function changeStar1(x){
	
	if($("#"+x).attr("src")==="Img/favMedium1.png"){
		$("#"+x).attr("src", "Img/favfullMedium.png");
		var idOglasa= x.substr(3);
		//console.log("MOJ ID JE:"+idOglasa);
		$.post({
			url:'rest/addToFav/'+idOglasa,
			success:function(){
				alert("Added to favourites");
				//da li treba ovaj refresh
				location.hash="mojHash";
				location.reload();
			},
			error:function(){
				alert('Error occured');
			}
		});
	}
	else{
		$("#"+x).attr("src", "Img/favMedium1.png");
		var idOglasa= x.substr(3);
		$.post({
			url:'rest/removeFromFav/'+idOglasa,
			success:function(){
				alert("Deleted from favourites");
				//da li treba ovaj refresh
				location.hash="mojHash";
				location.reload();
			},
			error:function(){
				alert('Error occured');
			}
		});
	}
}

//*************************
//GOTOVE NEKE FUNCKIJE
//************************





// Closure koji će zapamtiti trenutni proizvod
function clickClosure(product){
	return function() {
		// Parametar product prosleđen u gornju funkciju će biti vidljiv u ovoj
		// Ovo znači da je funkcija "zapamtila" za koji je proizvod vezana
		$('tr.selected').removeClass('selected');
		$(this).addClass('selected');
	};
}

function addProductTr(product) {
	let tr = $('<tr></tr>');
	let tdNaziv = $('<td>' + product.name + '</td>');
	let tdCena = $('<td>' + product.price + '</td>');
	tr.append(tdNaziv).append(tdCena);
	tr.click(clickClosure(product));
	$('#tabela tbody').append(tr);
}







//*****************DOCUMENT READY***********************
$(document).ready(function() {
	/*$.get({
		url: 'rest/products',
		success: function(products) {
			for (let product of products) {
				addProductTr(product);
			}
		}
	});*/
	
	
	//PROBA SA HASH ONLOAD
	//KAD PROMENIS FAVOURITES IZ FAVITEMS DA OSTANE TU ALI REFRESHUJE
	if(location.hash=="#mojHash"){
		//alert();
		setTimeout(function(){
			//$('#favourite_ads_buyer')[0].click();
			$('#favourite_ads_buyer').trigger('click');

		},200)

		//$('#favourite_ads_buyer').trigger('click');
	}
	
	if(location.hash=="#mojHashDeleteRec"){
		setTimeout(function(){
			$('#postedReviews').trigger('click');
		},200)
	}
	
	
	
	//Top 10 ads
	$('#top10').click(function(e){
		e.preventDefault();
		
		$('#myMessages').attr("hidden",true);
		$('#myInbox').attr("hidden",true);
		$("#categoriesItems").attr("hidden",true);
		$('#addCategory').attr("hidden", true);
		$("#addAdvertisement").attr("hidden",true);
		$("#advertisementItems").attr("hidden",false);
		$("#advertisementItemsBuyer").attr("hidden",true);
		$('#addRecension').attr("hidden",true);
		$('#editAd').attr("hidden",true);
		$('#postedRecensions').attr("hidden",true);
		$('#editCategory').attr("hidden",true);
		$('#editRecension').attr("hidden",true);
		
		$("#advertisementItems").html('');
		
		$("#advertisementItems").html("<div class=\"col-sm-12 col-md-12 col-lg-12\"><h3 style=\"float:center;\">Top 10 most popular ads</h3><br></div>");
		
		let mojaLista = [];
		let mojTemp=[];
		let kupci = [];
		let pom;
		for(h=0; h<allAds.length; h++){
			if(allAds[h].active){
				mojTemp.push(allAds[h]);
			}
		}
		
		for(k=0; k<allU.length; k++){
			if(allU[k].userRole==0){
				kupci.push(allU[k]);
			}
		}
		
		let dict= {};
		//napravi recnik i napuni ga sa kljucevima i nulama
		for(n=0; n<kupci.length; n++){
			if(kupci[n].advertisementsFavouritesBuyer!=null){
				for(t=0; t<kupci[n].advertisementsFavouritesBuyer.length; t++){
					if(dict[kupci[n].advertisementsFavouritesBuyer[t]]!=0){
						dict[kupci[n].advertisementsFavouritesBuyer[t]]=0;
					}
					//console.log("EVO \n"+kupci[n].advertisementsFavouritesBuyer[t]);
				}
			}
		}
		
		//svaki put kad se id pojavi u listi fav kupca povecaj tom kljuc vrednost paru za 1
		for(n=0; n<kupci.length; n++){
			//console.log(kupci[n]);
			if(kupci[n].advertisementsFavouritesBuyer!=null){
				
				for(t=0; t<kupci[n].advertisementsFavouritesBuyer.length; t++){
					//alert('povecao');
					//console.log(kupci[n].advertisementsFavouritesBuyer[t]);
					dict[kupci[n].advertisementsFavouritesBuyer[t]]+=1;
					//console.log(dict[kupci[n].advertisementsFavouritesBuyer[t]]);
				}
			}
		}
		
		let nizz = [];
		//sad je napravljen recnik gde su oglasi u paru sa brojem favrecenzija
		for(let kljuc in dict){
			nizz.push(dict[kljuc]+"TERMSTRING"+kljuc);
			//console.log(dict[kljuc]+"TERMSTRING"+kljuc);
		}
		let zameni;
		for(y=0; y<nizz.length-1; y++){
			//console.log(nizz[y]);
			//console.log(nizz[2]);
			for(s=y+1; s<nizz.length; s++){
				let delovi = nizz[s].split("TERMSTRING");
				let delovi2 = nizz[y].split("TERMSTRING");
				//console.log("DELOVI "+delovi2[0]);
				if(delovi[0]>delovi2[0]){
					zameni=nizz[s];
					nizz[s]=nizz[y];
					nizz[y]=zameni;
				}
			}
		}
		let idSortirani = [];
		for(brojac=0; brojac<nizz.length; brojac++){
			let deo = nizz[brojac].split("TERMSTRING");
			idSortirani.push(deo[1]);
			//console.log("Sort "+idSortirani[brojac]);
			
		}
		
		let finOglasi=[];
		for(br=0; br<allAds.length; br++){
			for(brr=0; brr<idSortirani.length; brr++){
				if(idSortirani[brr]==allAds[br].id && allAds[br].active){
					finOglasi.push(allAds[br]);
					//console.log(allAds[br]);
				}
			}
		}
		
		for(a=0; a<finOglasi.length; a++){
			appendAdvertisementItems(finOglasi[a],0);
			console.log(finOglasi[a]);
			
			if(a>9){
				break;
			}
		}
		
		
		//let nizIdeva = [];
		//for(h=0; h<finOglasi.length; h++){
			//nizIdeva.push(finOglasi.id);
		//}
		if(finOglasi.length<10 && finOglasi.length>0){
			let mojBroj=finOglasi.length;
			
			for(brr=0; brr<allAds.length; brr++){
				if(!finOglasi.includes(allAds[brr])){
					appendAdvertisementItems(allAds[brr],0);
					mojBroj++;
				}
				
				if(mojBroj>9){
					return;
				}
			}
		}
		
		if(finOglasi.length==0){
			let mojBroj=0;
			
			for(brr=0; brr<allAds.length; brr++){
				appendAdvertisementItems(allAds[brr],0);
				mojBroj++;
				
				if(mojBroj>9){
					break;
				}
			}
		}
		/*if(finOglasi!=null && finOglasi!=undefined){
			
		}
		else{
			$("#advertisementItems").html("<p>No active ads</p>");
		}*/
		
		let mojaPromenljiva=0;
		for(br=0; br<allAds.length; br++){
			if(allAds[br].active==true){
				mojaPromenljiva=1;
			}
		}
		if(mojaPromenljiva==0){
			$("#advertisementItems").html('');
			$("#advertisementItems").html("<p>No active ads</p>");
		}



	});
	
	//CLICK NA CREATE RECENSION FORMU DA SAKRIJE SVE OSTALE
	$('#create_review').click(function(e){
		e.preventDefault();
		
		$('#myMessages').attr("hidden",true);
		$('#myInbox').attr("hidden",true);
		$("#categoriesItems").attr("hidden",true);
		$('#addCategory').attr("hidden", true);
		$("#addAdvertisement").attr("hidden",true);
		$("#advertisementItems").attr("hidden",true);
		$("#advertisementItemsBuyer").attr("hidden",true);
		$('#postedRecensions').attr("hidden",true);
		$('#addRecension').attr("hidden",false);
		
		$('#adNameRecension').html('');
		$('#adNameRecension').html("<option value=\"\" disabled selected hidden=\"true\">Please select ad...</option>");

		appendAdRecension();
	});
	//CLICK NA ADD RECENSION SUBMIT
	$('#formRecension').submit(function(e){
		e.preventDefault();
		
		//alert('UDJE');
		
		//imgInBase641
		let imeOglasaRec = $('#adNameRecension option:selected').text();
		let idOgl = $('#adNameRecension option:selected').val();

		console.log(imeOglasaRec);
		let naslovRec = $('#titleRecension').val();
		console.log(naslovRec);
		let contentRec = $('#descriptionRecension').val();
		console.log(contentRec);
		let slikaRec = imgInBase641;
		let opisOglasaOk =     document.getElementById("recensionDescriptionCb").checked;
		console.log(opisOglasaOk);
		let dogovorOglas =     document.getElementById("recensionDealCb").checked;
		console.log(dogovorOglas);
		
		if(imeOglasaRec==null || imeOglasaRec==undefined || imeOglasaRec==""){
			alert('Ad name must be selected');
			return;
		}
		if(naslovRec==null || naslovRec==undefined || naslovRec==""){
			alert('Title must be filled');
			return;
		}
		if(contentRec==null || contentRec==undefined || contentRec==""){
			alert('Description must be filled');
			return;
		}
		
		if(slikaRec==null || slikaRec==undefined || slikaRec==""){
			slikaRec="NemaSliku";
		}
		
		let prijaviProdavca = document.getElementById("reportCb").checked;
		console.log(prijaviProdavca);
		
		
		
		let pom1=0;
		let pom2=0;
		
		if (document.getElementById('recLikeProd').checked) {
			pom2=1;
		}
		if (document.getElementById('recDislikeProd').checked) {
			pom2=-1;
		}
		
		if (document.getElementById('sellerLike').checked) {
			pom1=1;
		}
		
		if (document.getElementById('sellerDislike').checked) {
			pom1=-1;
		}
		
		//console.log('\n\n\nMOJI PODACI REC\n'+"ImeOglasa:"+imeOglasaRec+ "\nAutor rec:"+ mojKorisnik.username+
			//	"\nNaslov: "+naslovRec+ "\nSadrzaj:"+contentRec+"\nSlika: "+slikaRec);
		
		 let daProbam = mojKorisnik.username;
		$.post({
			url:'rest/rec/addRec/'+pom1+'/'+pom2+'/'+idOgl+'/'+prijaviProdavca,
			data: JSON.stringify({"id":uuidv4(),"ad":imeOglasaRec,"recAuthor":daProbam,
				"title":naslovRec,"content":contentRec,"imgPath":slikaRec,"dealFulfilled":dogovorOglas,
				"adDescriptionCorrect":opisOglasaOk}),

			contentType: 'application/json',
			
			
			success:function(){
				alert('Recension posted');
				//window.loaction='./webshop.html';
				
				window.location.reload();

			},
			error:function(){
				alert('Error');
			}
		});
		
		
	});
	
	
	//CLICK NA EDIT RECENSION SUBMIT
	$('#formRecensionEdit').submit(function(e){
		e.preventDefault();
		
		//alert('UDJE');
		
		//imgInBase641
		//let imeOglasaRec = $('#adNameRecension option:selected').text();
		//let idOgl = $('#adNameRecension option:selected').val();

		//console.log(imeOglasaRec);
		
		let naslovRec = $('#titleRecensionEdit').val();
		console.log(naslovRec);
		let contentRec = $('#descriptionRecensionEdit').val();
		console.log(contentRec);
		let slikaRec = imgInBase645;
		let opisOglasaOk =     document.getElementById("recensionDescriptionCbEdit").checked;
		console.log(opisOglasaOk);
		let dogovorOglas =     document.getElementById("recensionDealCbEdit").checked;
		console.log(dogovorOglas);
		
		//if(imeOglasaRec==null || imeOglasaRec==undefined || imeOglasaRec==""){
		//	alert('Ad name must be selected');
		//	return;
		//}
		if(naslovRec==null || naslovRec==undefined || naslovRec==""){
			alert('Title must be filled');
			return;
		}
		if(contentRec==null || contentRec==undefined || contentRec==""){
			alert('Description must be filled');
			return;
		}
		
		if((slikaRec==null || slikaRec==undefined || slikaRec=="") && mojaRec.imgPath=="NemaSliku"){
			slikaRec="NemaSliku";
		}
		
		if(mojaRec.imgPath!="NemaSliku" && (slikaRec==null || slikaRec==undefined || slikaRec=="")){
			slikaRec=mojaRec.imgPath;
		}
		
		
		// let daProbam = mojKorisnik.username;
		$.post({
			url:'rest/rec/editRec/'+naslovRec+'/'+contentRec+'/'+opisOglasaOk+'/'+dogovorOglas+'/'+mojaRec.id,
			data:JSON.stringify({slikaRec}),
			contentType: 'application/json',

			success:function(){
				alert('Recension edited');
				//window.loaction='./webshop.html';
				
				window.location.reload();

			},
			error:function(){
				alert('Error');
			}
		});
		
		
	});
	
	
	//CLICK NA ordered_ads_buyer delivered_ads_buyer i favourite_ads_buyer
	$("#ordered_ads_buyer").click(function(e){
		e.preventDefault();
		
		$('#myMessages').attr("hidden",true);
		$('#myInbox').attr("hidden",true);
		$("#categoriesItems").attr("hidden",true);
		$('#addCategory').attr("hidden", true);
		$("#addAdvertisement").attr("hidden",true);
		$("#advertisementItems").attr("hidden",true);
		$("#advertisementItemsBuyer").attr("hidden",false);
		$('#addRecension').attr("hidden",true);
		$('#editAd').attr("hidden",true);
		$('#editCategory').attr("hidden",true);
		$('#editCategory').attr("hidden",true);
		$('#editRecension').attr("hidden",true);
		$('#postedRecensions').attr("hidden",true);






		
		$("#advertisementItemsBuyer").html('');

		let niz=[];
		niz=mojKorisnik.advertisementsOrderedBuyer;
		console.log(niz);
		
		$.get({
			url:'rest/ads/getAds',
			success: function(ads){
				allAds=ads;
				
				
				if(niz==null || niz==undefined || niz.length==0){
					$('#advertisementItemsBuyer').append(
							"<div style=\"width:100%\"><h3 align=\"center\">There are no ordered items</h3></div>"
					);
					return;
				}
				for( i=0; i<allAds.length; i++){
					if(niz.includes(allAds[i].id)){
						appendOrderedAdsBuyer(allAds[i]);
					}
				}
			}
		});
		
		
	});
	$("#delivered_ads_buyer").click(function(e){
		e.preventDefault();
		
		$('#myMessages').attr("hidden",true);
		$('#myInbox').attr("hidden",true);
		$("#categoriesItems").attr("hidden",true);
		$('#addCategory').attr("hidden", true);
		$("#addAdvertisement").attr("hidden",true);
		$("#advertisementItems").attr("hidden",true);
		$("#advertisementItemsBuyer").attr("hidden",false);
		$('#addRecension').attr("hidden",true);
		$('#editAd').attr("hidden",true);
		$('#editCategory').attr("hidden",true);
		$('#editRecension').attr("hidden",true);
		$('#postedRecensions').attr("hidden",true);




		
		$("#advertisementItemsBuyer").html('');
		
		let niz=[];
		niz=mojKorisnik.advertisementsDeliveredBuyer;
		console.log(niz);
		
		$.get({
			url:'rest/ads/getAds',
			success: function(ads){
				allAds=ads;
				
				advertisementItemsBuyer
				if(niz==null || niz==undefined || niz.length==0){
					$('#advertisementItemsBuyer').append(
							"<div style=\"width:100%\"><h3 align=\"center\">There are no delivered items</h3></div>"
					);
					return;
				}
				for( i=0; i<allAds.length; i++){
					if(niz.includes(allAds[i].id)){
						appendDeliveredAdsBuyer(allAds[i]);
					}
				}
			}
		});
		
		
	});
	$("#favourite_ads_buyer").click(function(e){
		e.preventDefault();
		
		$('#myMessages').attr("hidden",true);
		$('#myInbox').attr("hidden",true);
		$("#categoriesItems").attr("hidden",true);
		$('#addCategory').attr("hidden", true);
		$("#addAdvertisement").attr("hidden",true);
		$("#advertisementItems").attr("hidden",true);
		$("#advertisementItemsBuyer").attr("hidden",false);
		$('#addRecension').attr("hidden",true);
		$('#editAd').attr("hidden",true);
		$('#editCategory').attr("hidden",true);
		$('#editRecension').attr("hidden",true);
		$('#postedRecensions').attr("hidden",true);




		
		$("#advertisementItemsBuyer").html('');
		let niz=[];
		niz=mojKorisnik.advertisementsFavouritesBuyer;
		console.log('U fav ads se nalaze:'+niz);
		
		$.get({
			url:'rest/ads/getAds',
			success: function(ads){
				allAds=ads;
				
				
				if(niz==null || niz==undefined || niz.length==0){
					$('#advertisementItemsBuyer').append(
							"<div style=\"width:100%\"><h3 align=\"center\">There are no favourite items</h3></div>"
					);
					return;
				}
				for( i=0; i<allAds.length; i++){
					if(niz.includes(allAds[i].id)){
						alert('NASAO ME');
						appendFavouritesBuyer(allAds[i]);
					}
				}
			}
		});
		
		
	});
	
	//CLICK NA INBOX DA PRIKAZE SVE PRIMLJENE PORUKE
	//POSEBAN CLICK ZA SVA TRI TIPA KORISNIKA
	$("#inbox_admin").click(function(e){
		e.preventDefault();
		
		$('#myMessages').attr("hidden",true);
		$('#myInbox').attr("hidden",false);
		$("#categoriesItems").attr("hidden",true);
		$('#addCategory').attr("hidden", true);
		$("#addAdvertisement").attr("hidden",true);
		$("#advertisementItems").attr("hidden",true);
		$("#advertisementItemsBuyer").attr("hidden",true);
		$('#addRecension').attr("hidden",true);
		$('#editAd').attr("hidden",true);
		$('#postedRecensions').attr("hidden",true);
		$('#editCategory').attr("hidden",true);
		$('#editRecension').attr("hidden",true);







		$('#myInbox').html('');
		
		var nemaPor=0;
		
		$.get({
			url:'rest/message/load',
			success:function(msgs){
				//console.log(msgs);
				allMsgs=msgs;
				
			}
		});
		
		//sortiraj poruke po vremenu kad su poslate
		setTimeout(function(){
			for(b=0; b<allMsgs.length-1; b++){
				for(c=b+1; c<allMsgs.length; c++){
					let temp;
					if(allMsgs[b].dateAndTime < allMsgs[c].dateAndTime){
						temp=allMsgs[b];
						allMsgs[b]=allMsgs[c];
						allMsgs[c]=temp;
					}
				}
			}
		},100);
			
		
		setTimeout(function(){
			for(r=0; r<allMsgs.length; r++){
				if(allMsgs[r].receiver==mojKorisnik.username && allMsgs[r].deleted==false){
					
					appendMyInbox(allMsgs[r]);
					nemaPor=1;
				}
			}
			
			if(nemaPor==0){
				$("#myInbox").append(
						"<h3 align=\"center\">Inbox is empty</h3>"
				);
			}
		},150);
		
	});
	$("#inbox_seller").click(function(e){
e.preventDefault();
		
		$('#myMessages').attr("hidden",true);
		$('#myInbox').attr("hidden",false);
		$("#categoriesItems").attr("hidden",true);
		$('#addCategory').attr("hidden", true);
		$("#addAdvertisement").attr("hidden",true);
		$("#advertisementItems").attr("hidden",true);
		$("#advertisementItemsBuyer").attr("hidden",true);
		$('#addRecension').attr("hidden",true);
		$('#editAd').attr("hidden",true);
		$('#postedRecensions').attr("hidden",true);
		$('#editCategory').attr("hidden",true);
		$('#editRecension').attr("hidden",true);







		$('#myInbox').html('');
		
		var nemaPor=0;
		
		$.get({
			url:'rest/message/load',
			success:function(msgs){
				//console.log(msgs);
				allMsgs=msgs;
				
			}
		});
		
		//sortiraj poruke po vremenu kad su poslate
		setTimeout(function(){
			for(b=0; b<allMsgs.length-1; b++){
				for(c=b+1; c<allMsgs.length; c++){
					let temp;
					if(allMsgs[b].dateAndTime < allMsgs[c].dateAndTime){
						temp=allMsgs[b];
						allMsgs[b]=allMsgs[c];
						allMsgs[c]=temp;
					}
				}
			}
		},100);
			
		
		setTimeout(function(){
			for(r=0; r<allMsgs.length; r++){
				if(allMsgs[r].receiver==mojKorisnik.username && allMsgs[r].deleted==false){
					
					appendMyInbox(allMsgs[r]);
					nemaPor=1;
				}
			}
			
			if(nemaPor==0){
				$("#myInbox").append(
						"<h3 align=\"center\">Inbox is empty</h3>"
				);
			}
		},150);
	});
	$("#inbox_buyer").click(function(e){
e.preventDefault();
		
		$('#myMessages').attr("hidden",true);
		$('#myInbox').attr("hidden",false);
		$("#categoriesItems").attr("hidden",true);
		$('#addCategory').attr("hidden", true);
		$("#addAdvertisement").attr("hidden",true);
		$("#advertisementItems").attr("hidden",true);
		$("#advertisementItemsBuyer").attr("hidden",true);
		$('#addRecension').attr("hidden",true);
		$('#editAd').attr("hidden",true);
		$('#postedRecensions').attr("hidden",true);
		$('#editCategory').attr("hidden",true);
		$('#editRecension').attr("hidden",true);







		$('#myInbox').html('');
		
		var nemaPor=0;
		
		$.get({
			url:'rest/message/load',
			success:function(msgs){
				//console.log(msgs);
				allMsgs=msgs;
				
			}
		});
		
		//sortiraj poruke po vremenu kad su poslate
		setTimeout(function(){
			for(b=0; b<allMsgs.length-1; b++){
				for(c=b+1; c<allMsgs.length; c++){
					let temp;
					if(allMsgs[b].dateAndTime < allMsgs[c].dateAndTime){
						temp=allMsgs[b];
						allMsgs[b]=allMsgs[c];
						allMsgs[c]=temp;
					}
				}
			}
		},100);
			
		
		setTimeout(function(){
			for(r=0; r<allMsgs.length; r++){
				if(allMsgs[r].receiver==mojKorisnik.username && allMsgs[r].deleted==false){
					
					appendMyInbox(allMsgs[r]);
					nemaPor=1;
				}
			}
			
			if(nemaPor==0){
				$("#myInbox").append(
						"<h3 align=\"center\">Inbox is empty</h3>"
				);
			}
		},150);
	});
	
	
	//NA MYMESSAGES DA PRIKAZE SVE POSLATE PORUKE OD TOG KORISNIKA
	//OVDE NISAM RADIO OPET AJAX POZIV DA UCITA AKO SE DESILA PROMENA
	//ISTO TRI CLICKA ZA TRI TIPA KORISNIKA
	$("#my_messages_admin").click(function(e){
		e.preventDefault();
		
		$('#myMessages').attr("hidden",false);
		$('#myInbox').attr("hidden",true);
		$("#categoriesItems").attr("hidden",true);
		$('#addCategory').attr("hidden", true);
		$("#addAdvertisement").attr("hidden",true);
		$("#advertisementItems").attr("hidden",true);
		$("#advertisementItemsBuyer").attr("hidden",true);
		$('#addRecension').attr("hidden",true);
		$('#editAd').attr("hidden",true);
		$('#postedRecensions').attr("hidden",true);
		$('#editCategory').attr("hidden",true);
		$('#editRecension').attr("hidden",true);







		$('#myMessages').html('');

		for(b=0; b<allMsgs.length-1; b++){
			for(c=b+1; c<allMsgs.length; c++){
				let temp;
				if(allMsgs[b].dateAndTime < allMsgs[c].dateAndTime){
					temp=allMsgs[b];
					allMsgs[b]=allMsgs[c];
					allMsgs[c]=temp;
				}
			}
		}
		
		var nemaPor=0;
		
		for(r=0; r<allMsgs.length; r++){
			if(allMsgs[r].sender==mojKorisnik.username && allMsgs[r].deleted==false){
				
				appendMyMessages(allMsgs[r]);
				nemaPor=1;
			}
		}
		
		if(nemaPor==0){
			$("#myMessages").append(
					"<h3 align=\"center\">Outbox is empty</h3>"
			);
		}
	});
	
	$("#my_messages_seller").click(function(e){
		e.preventDefault();
		
		$('#myMessages').attr("hidden",false);
		$('#myInbox').attr("hidden",true);
		$("#categoriesItems").attr("hidden",true);
		$('#addCategory').attr("hidden", true);
		$("#addAdvertisement").attr("hidden",true);
		$("#advertisementItems").attr("hidden",true);
		$("#advertisementItemsBuyer").attr("hidden",true);
		$('#addRecension').attr("hidden",true);
		$('#editAd').attr("hidden",true);
		$('#postedRecensions').attr("hidden",true);
		$('#editCategory').attr("hidden",true);
		$('#editRecension').attr("hidden",true);






		$('#myMessages').html('');

		
		for(b=0; b<allMsgs.length-1; b++){
			for(c=b+1; c<allMsgs.length; c++){
				let temp;
				if(allMsgs[b].dateAndTime < allMsgs[c].dateAndTime){
					temp=allMsgs[b];
					allMsgs[b]=allMsgs[c];
					allMsgs[c]=temp;
				}
			}
		}
		
		var nemaPor=0;

		for(r=0; r<allMsgs.length; r++){
			if(allMsgs[r].sender==mojKorisnik.username && allMsgs[r].deleted==false){
				
				appendMyMessages(allMsgs[r]);
				nemaPor=1;
			}
		}
		
		if(nemaPor==0){
			$("#myMessages").append(
					"<h3 align=\"center\">Outbox is empty</h3>"
			);
		}
	});
	
	$("#my_messages_buyer").click(function(e){
		e.preventDefault();
		
		$('#myMessages').attr("hidden",false);
		$('#myInbox').attr("hidden",true);
		$("#categoriesItems").attr("hidden",true);
		$('#addCategory').attr("hidden", true);
		$("#addAdvertisement").attr("hidden",true);
		$("#advertisementItems").attr("hidden",true);
		$("#advertisementItemsBuyer").attr("hidden",true);
		$('#addRecension').attr("hidden",true);
		$('#editAd').attr("hidden",true);
		$('#postedRecensions').attr("hidden",true);
		$('#editCategory').attr("hidden",true);
		$('#editRecension').attr("hidden",true);







		$('#myMessages').html('');
		
		for(b=0; b<allMsgs.length-1; b++){
			for(c=b+1; c<allMsgs.length; c++){
				let temp;
				if(allMsgs[b].dateAndTime < allMsgs[c].dateAndTime){
					temp=allMsgs[b];
					allMsgs[b]=allMsgs[c];
					allMsgs[c]=temp;
				}
			}
		}
		
		var nemaPor=0;


		for(r=0; r<allMsgs.length; r++){
			if(allMsgs[r].sender==mojKorisnik.username && allMsgs[r].deleted==false){
				
				appendMyMessages(allMsgs[r]);
				nemaPor=1;
			}
		}
		
		if(nemaPor==0){
			$("#myMessages").append(
					"<h3 align=\"center\">Outbox is empty</h3>"
			);
		}
	});
	
	
	
	//NA COMPOSE NAPUNI SELECT USER I SELECT ADD KOD PORUKE
	//PONOVO TRI CLICKA ZA TRI RAZLICITA KORISNIKA
	$('#compose_admin').click(function(e){
		e.preventDefault(e);
		
		$.get({
			url:'rest/getUsers',
			success: function(users){
				var allU=users;
				$('#select_receiver').html('');
				$('#select_receiver').append("<option value=\"\" disabled selected hidden=\"true\">Please select user...</option>");
				
				for( i=0; i<allU.length; i++){
					//console.log(allU[i].username);
					//console.log(allU[i]);
					appendUsersMsg(allU[i]);
				}
			}
		});
		
		appendAdsMsg();
	});
	$('#compose_seller').click(function(e){
		e.preventDefault(e);
		
		$.get({
			url:'rest/getUsers',
			success: function(users){
				var allU=users;
				$('#select_receiver').html('');
				$('#select_receiver').append("<option value=\"\" disabled selected hidden=\"true\">Please select user...</option>");
				
				for( i=0; i<allU.length; i++){
					//console.log(allU[i].username);
					//console.log(allU[i]);
					if(allU[i].userRole==1){
						appendUsersMsg(allU[i]);
					}
				}
			}
		});
		
		appendAdsMsg();
	});
	
	$('#compose_buyer').click(function(e){
		e.preventDefault(e);
		
		
		//alert("DESIM SE")
		$.get({
			url:'rest/getUsers',
			success: function(users){
				var allU=users;
				$('#select_receiver').html('');
				$('#select_receiver').append("<option value=\"\" disabled selected hidden=\"true\">Please select user...</option>");
				
				for( i=0; i<allU.length; i++){
					//console.log(allU[i].username);
					//console.log(allU[i]);
					if(allU[i].userRole==2){
						appendUsersMsg(allU[i]);
					}
				}
			}
		});
		
		appendAdsMsg();
	});
	
	
	
	
	
	//CATEGORY DA PRIKAZE KATEGORIJE NA CLICK NA SHOW CATEGORIES
	$('#show_categories').click(function(e){
		e.preventDefault();
		
		$("#categoriesItems").attr("hidden",false);
		$('#advertisementItems').attr("hidden", true);
		$('#addCategory').attr("hidden", true);
		$("#categoriesItems").attr("hidden",false);
		$('#myMessages').attr("hidden",true);
		$('#myInbox').attr("hidden",true);
		$("#advertisementItemsBuyer").attr("hidden",true);
		$('#postedRecensions').attr("hidden",true);
		$('#addRecension').attr("hidden",true);
		$('#editRecension').attr("hidden",true);
		$('#editCategory').attr("hidden",true);





		
		$.get({
			url:'rest/category/getCats',
			success:function(categories){
				allCats1=categories;
				//DA OBRISE SVE IZ TOGA TAGA
				$('#categoriesItems').html('');
				for(k=0; k<categories.length; k++){
					appendCategories(categories[k]);
				}
			}
		});

	});
	
	//DA PRIKAZE POSTOVANE REVIEWS NA CLICK POSTED REVIEWS
	$('#postedReviews').click(function(e){
		e.preventDefault();
		
		$("#categoriesItems").attr("hidden",true);
		$('#advertisementItems').attr("hidden", true);
		$('#addCategory').attr("hidden", true);
		$("#categoriesItems").attr("hidden",false);
		$('#myMessages').attr("hidden",true);
		$('#myInbox').attr("hidden",true);
		$("#advertisementItemsBuyer").attr("hidden",true);
		$('#addRecension').attr("hidden",true);
		$('#editRecension').attr("hidden",true);
		$('#postedRecensions').attr("hidden",false);
		
		$.get({
			url:'rest/rec/getRecs',
			success: function(recs){
				$('#postedRecensions').html('');
				
				let nemaIh=1;
				for(k=0; k<recs.length; k++){
					if(recs[k].recAuthor==mojKorisnik.username){
						appendRecs(recs[k]);
						nemaIh=0;
					}
				}
				if(nemaIh==1){
					$('#postedRecensions').html("<h3 style=\"float:center;\">List of posted reviews is empty</h3>");
				}
			}
		});

	});
	
	//DA PRIKAZE DOBIJENE REVIEWS NA CLICK REVIEWS RECEIVED
	$('#reviewsReceived').click(function(e){
		e.preventDefault();
		//u isti div cu stavljati jer se prodavac i kupac iskljucuju medjusobno
		$("#categoriesItems").attr("hidden",true);
		$('#advertisementItems').attr("hidden", true);
		$('#addCategory').attr("hidden", true);
		$("#categoriesItems").attr("hidden",false);
		$('#myMessages').attr("hidden",true);
		$('#myInbox').attr("hidden",true);
		$("#advertisementItemsBuyer").attr("hidden",true);
		$('#addRecension').attr("hidden",true);
		$('#editRecension').attr("hidden",true);
		$('#postedRecensions').attr("hidden",false);
		
		$.get({
			url:'rest/rec/getRecs',
			success: function(recs){
				$('#postedRecensions').html('');
				
				let nemaIh=1;

				let mojOglas=null;
				
				for(r=0; r<allAds.length; r++){
					for(p=0; p<recs.length; p++){
						if(allAds[r].recensions!=null){
							for(t=0; t<allAds[r].recensions.length; t++ ){
								if(allAds[r].recensions[t]==recs[p].id){
									mojOglas=allAds[r];
									
									if(mojKorisnik.advertisementsSentSeller!=null){
										for(q=0; q<mojKorisnik.advertisementsSentSeller.length; q++){
											if(mojKorisnik.advertisementsSentSeller[q]==mojOglas.id){
												alert('Udje');
												nemaIh=0;
												appendRecs(recs[p]);
											}
										}
									}
								}
							}
						}
					}
				}
				
				//if(mojOglas!=null){
				//	alert('Nasao ga');
				//}



									//if(mojKorisnik.advertisementsPostedSeller[h]==recs[k].id){
										//alert('UDJE');
										//appendRecs(recs[k]);
										//nemaIh=0;
									//}
								
							
						
					
					
				
				
				if(nemaIh==1){
					$('#postedRecensions').html("<h3 style=\"float:center;\">List of received reviews is empty</h3>");
				}
			}
		});

	});
	
	//PRIKAZI FORMU ZA DODAVANJE KATEGORIJE
	$('#create_category').click(function(e){
		e.preventDefault();
		
		$('#advertisementItems').attr("hidden", true);
		$('#addCategory').attr("hidden", false);
		$("#categoriesItems").attr("hidden",true);
		$('#myMessages').attr("hidden",true);
		$('#myInbox').attr("hidden",true);
		$("#advertisementItemsBuyer").attr("hidden",true);
		$('#postedRecensions').attr("hidden",true);
		$('#addRecension').attr("hidden",true);
		$('#editRecension').attr("hidden",true);





	});
	
	
	
	//LOGOUT
	$('#btn_logout').click(function(){
		$.post({
			url:'rest/logout',
			success:function(){
			window.location='./login.html';
	        }
		});
	});
	
	
	//PODESI DA MU DEFAULT SELECT RADI NA USERROLE CHANE ROLE
	$('#select_user').on('change', function() {
		 if(this.value == 0){
		 $('#inlineRadioBuyer').prop("checked", true);
		 }else if(this.value == 1){
		 $('#inlineRadioAdmin').prop("checked", true);
		 }else if(this.value == 2){
		 $('#inlineRadioSeller').prop("checked", true);
		 }
		});
	
	
	//DODAJ CATEGORY
	$("#cat_forma").submit(function(e){
		e.preventDefault();
		
		let imeCat = $("#cat_name").val();
		let opisCat = $("#cat_description").val();
		let oglasiCat = $("#cat_select").val();
		
		let finOglasi=[];
		//console.log(oglasiCat);
		for(q=0; q<allAds.length; q++){
			for(w=0; w<oglasiCat.length; w++){
				if(allAds[q].id==oglasiCat[w]){
					finOglasi.push(allAds[q]);
				}
			}
		}
		if(imeCat=="" || opisCat=="" || finOglasi.length==0 || imeCat==null || imeCat==undefined ||
				opisCat==null || opisCat==undefined || finOglasi==null || finOglasi==undefined){
			alert('All fields must be filled');
			return;
		}
		console.log(finOglasi);
		
		$.post({
			url:'rest/category/addCat',
			data: JSON.stringify({
				"id":uuidv4(), "name":imeCat,"description":opisCat,"advertisements":finOglasi, "active":true
			}),
			contentType: 'application/json',
			success:function(){
				alert('uspesno dodao cat');
				window.location='./webshop.html'
			},
			error:function(){
				alert('Greska kod dodavanja cat')
			}
		});
	});
	
	//EDIT CATEGORY SUBMIT
	//NECE DA PROSLEDI ARRAY KAO PATHPARAM
	$('#cat_formaEdit').submit(function(e){
		e.preventDefault();
		
		let imeC = $('#cat_nameEdit').val();
		let opisC = $('#cat_descriptionEdit').val();
		let oglasiC = $('#cat_selectEdit').val();
		
		let finO = [];
		
		for(q=0; q<allAds.length; q++){
			for(w=0; w<oglasiC.length; w++){
				if(allAds[q].id==oglasiC[w]){
					finO.push(allAds[q]);
				}
			}
		}
		
		if(imeC==null || imeC=="" || imeC==undefined || opisC=="" || opisC==null || opisC==undefined
				|| finO==null || finO.length==0 || finO==undefined){
			alert('All fields must be filled');
			return;
		}
		
		$.post({
			url:'rest/category/editCat/'+mojIdCat,
			data: JSON.stringify({
				"id":uuidv4(), "name":imeC,"description":opisC,"advertisements":finO, "active":true
			}),
			contentType: 'application/json',
			success:function(){
				alert('Category edited');
				window.location.reload();
			},
			error:function(){
				alert('Error');
			}
		});
		
		
	});
	
	
	//PRETRAGA ZA KORISNIKE
	//nameSearchUser
	//gradSearchUser
	$('#form_pretraga_user').submit(function(e){
		e.preventDefault();
		
		$('#myMessages').attr("hidden",true);
		$('#myInbox').attr("hidden",true);
		$("#categoriesItems").attr("hidden",true);
		$('#addCategory').attr("hidden", true);
		$("#addAdvertisement").attr("hidden",true);
		$("#advertisementItems").attr("hidden",false);
		$("#advertisementItemsBuyer").attr("hidden",true);
		$('#addRecension').attr("hidden",true);
		$('#editRecension').attr("hidden",true);
		$('#editAd').attr("hidden",true);
		$('#postedRecensions').attr("hidden",true);
		$('#editCategory').attr("hidden",true);




		
		//obrisi sve oglase i prikazi samo one korisnike
		//koji zadovoljavaju kriterijum pretrage
		$('#advertisementItems').html('');
		
		$('#advertisementItems').html('<h3 style=\" margin-left:40%; float:center;\">Nothing found</h3>');
		
		let tempIme = [];
		let tempGrad = [];
		
		
		console.log("MOJI PODACI PRETRAGA USER\n\n\n")
		let imePretraga=$('#nameSearchUser').val();
		console.log("Korisnicko ime: "+imePretraga);
		if(imePretraga!=null && imePretraga!=undefined && imePretraga!=""){
			for(q=0; q<allU.length; q++){
				if(allU[q].username.includes(imePretraga)){
					tempIme.push(allU[q]);
					
				}
			}
			if(tempIme.length==0){
				console.log("Nema sa takvim korisnickim imenom korisnika");
				return;
			}
		}
		console.log(tempIme);
		
		let gradPretraga = $('#gradSearchUser :selected').text();
		if(gradPretraga=="Choose..."){
			gradPretraga="";
		}
		console.log("\nGrad: "+gradPretraga);
		//gradPretraga="";
		if(gradPretraga!=null && gradPretraga!=undefined && gradPretraga!=""){
			for(q=0; q<allU.length; q++){
				if(allU[q].city==gradPretraga){
					tempGrad.push(allU[q]);
				}
			}
			if(tempGrad.length==0){
				console.log("Nema oglasa iz tog grada");
				return;
			}
		}
		console.log(tempGrad);
		
		let resFin = [];
		let flag1=0;
		
		if(tempIme!=null && tempIme!=undefined && tempGrad != null && tempGrad!= undefined && tempIme.length!=0 && tempGrad.length!=0){
			for(q=0; q<tempIme.length; q++){
				if(tempGrad.includes(tempIme[q])){
					resFin.push(tempIme[q]);
					flag1 = 1;
				}
			}
			if(flag1==0){
				console.log("IZASAO FINALE USER PRETRAGA");

				return;
			}
		}
		else if(tempIme != null && tempIme != undefined && tempIme.length!=0){
			resFin = tempIme;
			flag1=1;
		}
		else if(tempGrad != null && tempGrad != undefined && tempGrad.length!=0){
			resFin = tempGrad;
			flag1=1;
		}
		else{
			console.log("I ime i grad su prazni");
		}
		
		if(resFin.length==0){
			console.log("Nista nije nasao od korisnika");
		}
		
		$('#advertisementItems').html('');
		for(q=0; q<resFin.length; q++){
			$('#advertisementItems').append(
					"<div class=\"card bg-light mb-3\" style=\"max-width: 18rem;\">"+
					  "<div class=\"card-header\">"+resFin[q].username+"</div>"+
					  "<div class=\"card-body\">"+
					    "<p class=\"card-text\">"+
					    	"<p>Name: "+resFin[q].firstName+" "+resFin[q].lastName+"</p>"+
					    	"<p>Role: "+resFin[q].role+"</p>"+
					    	"<p>City: "+resFin[q].city+"</p>"+
					    	"<p>Phone: "+resFin[q].phone+"</p>"+
					    	"<p>Email: "+resFin[q].email+"</p>"+
					  "</div>"+
					"</div>"
			);

		}
		
		
	});
	
	
	//PRETRAGA ZA OGLASE
	$('#form_pretraga').submit(function(e){
		e.preventDefault();
		
		$('#myMessages').attr("hidden",true);
		$('#myInbox').attr("hidden",true);
		$("#categoriesItems").attr("hidden",true);
		$('#addCategory').attr("hidden", true);
		$("#addAdvertisement").attr("hidden",true);
		$("#advertisementItems").attr("hidden",false);
		$("#advertisementItemsBuyer").attr("hidden",true);
		$('#addRecension').attr("hidden",true);
		$('#editRecension').attr("hidden",true);
		$('#editAd').attr("hidden",true);
		$('#postedRecensions').attr("hidden",true);
		$('#editCategory').attr("hidden",true);




		
		//obrisi sve oglase i prikazi samo one
		//koji zadovoljavaju kriterijum pretrage
		$('#advertisementItems').html('');
		
		$('#advertisementItems').html('<h3 style=\" margin-left:40%; float:center;\">Nothing found</h3>');

		
		let tempIme = [];
		let tempMinCena = [];
		let tempMaxCena = [];
		let tempMinLikes = [];
		let tempMaxLikes = [];
		let tempMinDatum = [];
		let tempMaxDatum = [];
		let tempGrad = [];
		let tempStatus = [];
		
		let fg1=0;
		let fg2=0;
		let fg3=0;
		let fg4=0;
		let fg5=0;
		let fg6=0;
		let fg7=0;
		let fg8=0;
		let fg9=0;
		
		let tempRes = [];
		
		console.log("MOJI PODACI \n\n\n")
		let imePretraga=$('#nameSearch').val();
		console.log("Ime: "+imePretraga);
		if(imePretraga!=null && imePretraga!=undefined && imePretraga!=""){
			for(q=0; q<allAds.length; q++){
				if(allAds[q].name.includes(imePretraga)){
					tempIme.push(allAds[q]);
					
				}
			}
			if(tempIme.length==0){
				console.log("Nema sa takvim imenom oglasa");
				return;
			}
		}
		console.log(tempIme);
		
		let cenaMinPretraga = $('#cenaMin').val();
		console.log("\nMin cena: "+cenaMinPretraga);
		if(cenaMinPretraga!=null && cenaMinPretraga!=undefined && cenaMinPretraga!=""){
			for(q=0; q<allAds.length; q++){
				if(allAds[q].price>=cenaMinPretraga){
					tempMinCena.push(allAds[q]);
				}
			}
			if(tempMinCena.length==0){
				console.log("Nema oglasa sa tom min cenom");
				return;
			}
		}
		console.log(tempMinCena);
		
		let cenaMaxPretraga = $('#cenaMax').val();
		console.log("\nMax cena: "+cenaMaxPretraga);
		if(cenaMaxPretraga!=null && cenaMaxPretraga!=undefined && cenaMaxPretraga!=""){
			for(q=0; q<allAds.length; q++){
				if(allAds[q].price<=cenaMaxPretraga){
					tempMaxCena.push(allAds[q]);
				}
			}
			if(tempMaxCena.length==0){
				console.log("Nema oglasa sa tom max cenom");
				return;
			}
		}
		console.log(tempMaxCena);
		
		let ocenaMinPretraga = $('#ocenaMin').val();
		console.log("\nMin ocena: "+ocenaMinPretraga);
		if(ocenaMinPretraga!=null && ocenaMinPretraga!=undefined && ocenaMinPretraga!=""){
			for(q=0; q<allAds.length; q++){
				if(allAds[q].numLikes>=ocenaMinPretraga){
					tempMinLikes.push(allAds[q]);
				}
			}
			if(tempMinLikes.length==0){
				console.log("Nema oglasa sa tim min likes");
				return;
			}
		}
		console.log(tempMinLikes);
		
		let ocenaMaxPretraga = $('#ocenaMax').val();
		console.log("\nMax ocena: "+ocenaMaxPretraga);
		if(ocenaMaxPretraga!=null && ocenaMaxPretraga!=undefined && ocenaMaxPretraga!=""){
			for(q=0; q<allAds.length; q++){
				if(allAds[q].numLikes<=ocenaMaxPretraga){
					tempMaxLikes.push(allAds[q]);
				}
			}
			if(tempMaxLikes.length==0){
				console.log("Nema oglasa sa tim max likes");
				return;
			}
		}
		console.log(tempMaxLikes);
		
		let datumMinPretraga = $('#datumMin').val();
		let datum123 = new Date(datumMinPretraga).getTime();
		//console.log("\n\n\n\n\n"+datum123+"\n\n\n\n\n");
		console.log("\nDatum min: "+datumMinPretraga);
		let nanVrednost = 1/0;
		//alert(datumMinPretraga + " kaze datum val");
		if(datumMinPretraga!=null && datumMinPretraga!=undefined && datumMinPretraga!="" && datum123!=nanVrednost){
			for(q=0; q<allAds.length; q++){
				if(allAds[q].dateExpired>=datum123){
					tempMinDatum.push(allAds[q]);
				}
			}
			if(tempMinDatum.length==0){
				console.log("Nema oglasa sa tim min datumom");
				return;
			}
		}
		console.log(tempMinDatum);
		
		let datumMaxPretraga = $('#datumMax').val();
		let datum1234 = new Date(datumMaxPretraga).getTime();
		console.log("\nDatum max: "+datumMaxPretraga);
		//alert(datumMaxPretraga + " kaze datum val");
		if(datumMaxPretraga!=null && datumMaxPretraga!=undefined && datumMaxPretraga!="" && datum1234!=nanVrednost){
			for(q=0; q<allAds.length; q++){
				if(allAds[q].dateExpired<=datum1234){
					tempMaxDatum.push(allAds[q]);
				}
			}
			if(tempMaxDatum.length==0){
				console.log("Nema oglasa sa tim max datumom");
				return;
			}
		}
		console.log(tempMaxDatum);
		
		
		let fgPretraga = -100;
		let statusPretraga = $('#statusSearch :selected').val();
		console.log("\nStatus: "+statusPretraga)
		if(statusPretraga!=null && statusPretraga!=undefined && statusPretraga!=""){
			for(q=0; q<allAds.length; q++){
				//ako je neregistrovan
				if(mojKorisnik==null){
					console.log("Oglas: "+allAds[q].id);
					if(allAds[q].status==statusPretraga){
						tempStatus.push(allAds[q]);
					}
					continue;
				}
				//ako je kupac u status mu prikazi samo koje je 
				//on porucio/koje su mu dostavljene
				if(mojKorisnik.userRole==0){
					if(allAds[q].status==statusPretraga){
						if(statusPretraga==0){
							tempStatus.push(allAds[q]);
						}
						else if(mojKorisnik.advertisementsOrderedBuyer!=null && mojKorisnik.advertisementsOrderedBuyer!=undefined && mojKorisnik.advertisementsOrderedBuyer!="" && statusPretraga==1){
						if(statusPretraga==1 && mojKorisnik.advertisementsOrderedBuyer.includes(allAds[q].id) && allAds[q].status==1){  //&& !(mojKorisnik.advertisementsDeliveredBuyer.includes(allAds[q].id))
							tempStatus.push(allAds[q]);
							fgPretraga=-10;
						}
						}
						else if(mojKorisnik.advertisementsDeliveredBuyer!=null && mojKorisnik.advertisementsDeliveredBuyer!=undefined && mojKorisnik.advertisementsDeliveredBuyer!=""){
						if(statusPretraga==2 && mojKorisnik.advertisementsDeliveredBuyer.includes(allAds[q].id)){
							tempStatus.push(allAds[q]);
							fgPretraga=-101;
						}
						}
					}
				}
				//ako admin pretrazuje prikazi mu sve
				if(mojKorisnik.userRole==1){
					if(allAds[q].status==statusPretraga){
						tempStatus.push(allAds[q]);
					}
				}
				//ako prodavac pretrazuje
				if(mojKorisnik.userRole==2){
					if(allAds[q].status==statusPretraga){
						if(mojKorisnik.advertisementsPostedSeller!=null && mojKorisnik.advertisementsPostedSeller!=undefined && mojKorisnik.advertisementsPostedSeller!=""){
						if((statusPretraga==0 || statusPretraga==1) && mojKorisnik.advertisementsPostedSeller.includes(allAds[q].id)){
							tempStatus.push(allAds[q]);
						}
						}
						else if(mojKorisnik.advertisementsSentSeller!=null && mojKorisnik.advertisementsSentSeller!=undefined && mojKorisnik.advertisementsSentSeller!=""){
						if(statusPretraga==2 && mojKorisnik.advertisementsSentSeller.includes(allAds[q].id)){
							tempStatus.push(allAds[q]);
						}
						}
						
					}
				}
				
			}
			if(tempStatus.length==0){
				console.log("Nema oglasa sa tim statusom");
				return;
			}
		}
		console.log(tempStatus);
		
		let gradPretraga = $('#gradSearch :selected').text();
		if(gradPretraga=="Choose..."){
			gradPretraga="";
		}
		console.log("\nGrad: "+gradPretraga);
		//gradPretraga="";
		if(gradPretraga!=null && gradPretraga!=undefined && gradPretraga!=""){
			for(q=0; q<allAds.length; q++){
				if(allAds[q].city==gradPretraga){
					tempGrad.push(allAds[q]);
				}
			}
			if(tempGrad.length==0){
				console.log("Nema oglasa iz tog grada");
				return;
			}
		}
		console.log(tempGrad);
		
		let tempRes1 = [];
		let tempRes2 = [];
		let tempRes3 = [];
		let tempRes4 = [];
		let tempRes5 = [];

		let flag1 = 0;
		let flag2 = 0;
		let flag3 = 0;
		let flag4 = 0;
		let flag5 = 0;

		

		//prvo kolo prvi mec
		if(tempIme!=null && tempIme!=undefined && tempMinCena != null && tempMinCena!= undefined && tempIme.length!=0 && tempMinCena.length!=0){
			for(q=0; q<tempIme.length; q++){
				if(tempMinCena.includes(tempIme[q])){
					tempRes1.push(tempIme[q]);
					flag1 = 1;
				}
			}
			if(flag1==0){
				console.log("IZASAO PRVI MEC PRVO KOLO");

				return;
			}
		}
		else if(tempIme != null && tempIme != undefined && tempIme.length!=0){
			tempRes1 = tempIme;
			flag1=1;
		}
		else if(tempMinCena != null && tempMinCena != undefined && tempMinCena.length!=0){
			tempRes1 = tempMinCena;
			flag1=1;
		}
		else{
			console.log("I ime i min cena su prazni");
		}
		
		//prvo kolo drugi mec
		if(tempMaxCena != null && tempMaxCena != undefined && tempMinLikes != null && tempMinLikes != undefined && tempMaxCena.length!=0 && tempMinLikes.length!=0){
			for(q=0; q<tempMaxCena.length; q++){
				if(tempMinLikes.includes(tempMaxCena[q])){
					tempRes2.push(tempMaxCena[q]);
					flag2=1;
				}
			}
			if(flag2==0){
				//console.log(tempMaxCena);
				//console.log(tempMinLikes);
				console.log("IZASAO DRUGI MEC PRVO KOLO");

				return;
			}
		}
		else if(tempMaxCena!= null && tempMaxCena != undefined && tempMaxCena.length!=0){
			tempRes2=tempMaxCena;
			flag2=1;
		}
		else if(tempMinLikes != null && tempMinLikes != undefined && tempMinLikes.length!=0){
			tempRes2 = tempMinLikes;
			flag2=1;
		}
		else{
			console.log("I max cena i min likes su prazni");
		}
		
		//prvo kolo treci mec
		if(tempMinDatum != null && tempMinDatum != undefined && tempMaxLikes != null && tempMaxLikes != undefined && tempMinDatum.length!=0 && tempMaxLikes.length!=0){
			for(q=0; q<tempMinDatum.length; q++){
				if(tempMaxLikes.includes(tempMinDatum[q])){
					tempRes3.push(tempMinDatum[q]);
					flag3=1;
				}
			}
			if(flag3==0){
				console.log("IZASAO PRVO KOLO TRECI MEC");

				return;
			}
		}
		else if(tempMinDatum != null && tempMinDatum != undefined && tempMinDatum.length!=0){
			tempRes3=tempMinDatum;
			flag3=1;
		}
		else if(tempMaxLikes != null && tempMaxLikes != undefined && tempMaxLikes.length!=0){
			tempRes3 = tempMaxLikes;
			flag3=1;
		}
		else{
			console.log("I max likes i min datum su prazni");
		}
		
		//prvo kolo cetvrti mec
		if(tempMaxDatum != null && tempMaxDatum != undefined && tempGrad != null && tempGrad != undefined && tempMaxDatum.length!=0 && tempGrad.length!=0){
			for(q=0; q<tempGrad.length; q++){
				if(tempMaxDatum.includes(tempGrad[q])){
					tempRes4.push(tempGrad[q]);
					flag4=1;
				}
			}
			if(flag4==0){
				console.log("IZASAO PRVO KOLO CETVRTI MEC");

				return;
			}
		}
		else if(tempMaxDatum != null && tempMaxDatum != undefined && tempMaxDatum.length!=0){
			tempRes4 = tempMaxDatum;
			flag4=1;
		}
		else if(tempGrad != null && tempGrad != undefined && tempGrad.length!=0){
			tempRes4 = tempGrad;
			flag4=1;
		}
		else{
			console.log("I grad i max datum su prazni");
		}
		
		//prvo kolo peti mec automatski prolaz
		if(tempStatus != null && tempStatus != undefined && tempStatus.length!=0){
			tempRes5 = tempStatus;
			flag5=1;
		}
		else{
			console.log("Status je prazan");
		}
		
		let tempRes11 = [];
		let tempRes12 = [];
		
		let flag11 = 0;
		let flag12 = 0;

		//drugo kolo prvi mec
		if(tempRes1 != null && tempRes1 != undefined && tempRes2 != null && tempRes2 != undefined && tempRes1.length!=0 && tempRes2.length!=0){
			for(q=0; q<tempRes1.length; q++){
				if(tempRes2.includes(tempRes1[q])){
					tempRes11.push(tempRes1[q]);
					flag11=1;
				}
			}
			if(flag11==0){
				console.log("IZASAO DRUGO KOLO PRVI MEC");

				return;
			}
		}
		else if(tempRes1 != null && tempRes1 != undefined && tempRes1.length!=0){
			tempRes11 = tempRes1;
			flag11=1;
		}
		else if(tempRes2 != null && tempRes2 != undefined && tempRes2.length!=0){
			tempRes11 = tempRes2;
			flag11=1;
		}
		
		//drugo kolo drugi mec
		if(tempRes3 != null && tempRes3 != undefined && tempRes4 != null && tempRes4 != undefined && tempRes3.length!=0 && tempRes4.length!=0){
			for(q=0; q<tempRes3.length; q++){
				if(tempRes4.includes(tempRes3[q])){
					tempRes12.push(tempRes3[q]);
					flag12=1;
				}
			}
			if(flag12==0){
				console.log("IZASAO DRUGO KOLO DRUGI MEC");

				return;
			}
		}
		else if(tempRes3 != null && tempRes3 != undefined && tempRes3.length!=0){
			tempRes12 = tempRes3;
			flag12=1;
		}
		else if(tempRes4 != null && tempRes4 != undefined && tempRes4.length!=0){
			tempRes12 = tempRes4;
			flag12=1;
		}
		
		
		 let tempRes21 = [];
		 
		 let flag123 = 0;
		//polufinale
		if(tempRes11 != null && tempRes11 != undefined && tempRes12 != null && tempRes12 != undefined && tempRes11.length!=0 && tempRes12.length!=0){
			for(q=0; q<tempRes11.length; q++){
				if(tempRes12.includes(tempRes11[q])){
					tempRes21.push(tempRes11[q]);
					flag123=1;
				}
			}
			if(flag123==0){
				console.log("IZASAO POLUFINALE");

				return;
			}
		}
		else if(tempRes11 != null && tempRes11 != undefined && tempRes11.length!=0){
			tempRes21 = tempRes11;
			flag123=1;
		}
		else if(tempRes12 != null && tempRes12 != undefined && tempRes12.length!=0){
			tempRes21 = tempRes12;
			flag123=1;
		}
		
		let flagFin=0;
		//finaleeeee
		if(tempRes21 != null && tempRes21 != undefined && tempRes5 != null && tempRes5 != undefined && tempRes21.length!=0 && tempRes5.length!=0){
			for(q=0; q<tempRes21.length; q++){
				if(tempRes5.includes(tempRes21[q])){
					tempRes.push(tempRes21[q]);
					flagFin=1;
				}
			}
			if(flagFin==0){
				console.log("IZASAO FINALE");

				return;
			}
		}
		else if(tempRes21 != null && tempRes21 != undefined && tempRes21.length!=0){
			tempRes = tempRes21;
			flagFin=1;
		}
		else if(tempRes5 != null && tempRes5 != undefined && tempRes5.length!=0){
			tempRes = tempRes5;
			flagFin=1;
		}
		
		
		if(tempRes!=null && tempRes!=undefined && tempRes.length!=0){
			$('#advertisementItems').html('');
		}
		
		console.log("DOSAO DO KRAJA PRETRAGE");
		
		for(q=0; q<tempRes.length; q++){
			if(fgPretraga==-10){
				appendAdvertisementItems(tempRes[q],fgPretraga);
			}
			else if(fgPretraga==-101){
				appendAdvertisementItems(tempRes[q],fgPretraga);
			}
			else{
				appendAdvertisementItems(tempRes[q],q);
			}
		}
		
		
		
		
	});
	
	
	
	//SIDEBAR
	 $('#sidebarCollapse').on('click', function () {
	        $('#sidebar').toggleClass('active');
	    });
	
	 //MODAL CANCEL I MODAL CLOSE DA SE RESETUJU POLJA ZA REPLY MSG
	 $('#modal_compose_close').click(function(){
		document.getElementById('reply_receiver').value=''; 
		document.getElementById('select_ad_reply').value=''; 
		$('#reply_title').val('');
		$('#reply_description').val('');
	});
		 
	 $('#modal_cancel_compose').click(function(){
		document.getElementById('reply_receiver').value=''; 
		document.getElementById('select_ad_reply').value=''; 
		$('#reply_title').val('');
		$('#reply_description').val('');
	});
	 
	 
	/* $('#modal_edit_close').click(function(){
			document.getElementById('select_ad_edit').value=''; 
			$('#reply_title').val('');
			$('#reply_description').val('');
		});
			 
		 $('#modal_cancel_compose').click(function(){
			document.getElementById('reply_receiver').value=''; 
			document.getElementById('select_ad_reply').value=''; 
			$('#reply_title').val('');
			$('#reply_description').val('');
		});*/
	 
	 //POSALJE PORUKU KUPCU OD PRODAVCA REPLY SAMO MOZE
	 $('#modal_confirm_reply').click(function(){
			
			if(document.getElementById('select_ad_reply').value==''){
				alert('All fields must be filled');
				return;
			}

			if($('#reply_description').val()==''){
				alert('All fields must be filled');
				return;
			}
			
			let uNameee = $('#reply_receiver').val();
			let adIddd = $('#select_ad_reply option:selected').text();
			let naslovMsgg = $('#reply_title').val();
			let opisMsgg = $('#reply_description').val();
			var datummm=new Date();
			
			
			$.post({
				url:'rest/message/compose',
				contentType: 'application/json',
				data: JSON.stringify({
					"id":uuidv4(), "receiver":uNameee, "adName":adIddd,"sender":mojKorisnik.username, "msgTitle":"Re: "+naslovMsgg,
					"msgContent":opisMsgg, "dateAndTime":datummm.getTime(), "read":false, "deleted":false
				}),
				success:function(){
					alert('Message sent');
					window.location.reload();
				},
				error:function(){
					alert('Error occured');
				}
			});
			 
		 });
	 
	 //MODAL CANCEL I MODAL CLOSE DA SE RESETUJU POLJA ZA COMPOSE MSG
	 $('#modal_compose_close').click(function(){
		document.getElementById('select_receiver').value=''; 
		document.getElementById('select_ad_compose').value=''; 
		$('#message_title').val('');
		$('#message_description').val('');
	 });
	 
	 $('#modal_cancel_compose').click(function(){
			document.getElementById('select_receiver').value=''; 
			document.getElementById('select_ad_compose').value=''; 
			$('#message_title').val('');
			$('#message_description').val('');
		 });
	 
	 //POSALJI PORUKU CLICK
	 $('#modal_confirm_compose').click(function(){
		if(document.getElementById('select_receiver').value==''){
			alert('All fields must be filled');
			return;
		}
		if(document.getElementById('select_ad_compose').value==''){
			alert('All fields must be filled');
			return;
		}
		if($('#message_title').val()==''){
			alert('All fields must be filled');
			return;
		}
		if($('#message_description').val()==''){
			alert('All fields must be filled');
			return;
		}
		
		let uNamee = $('#select_receiver option:selected').text();
		let adIdd = $('#select_ad_compose option:selected').text();
		let naslovMsg = $('#message_title').val();
		let opisMsg = $('#message_description').val();
		
		var datumm=new Date();
				
		$.post({
			url:'rest/message/compose',
			contentType: 'application/json',
			data: JSON.stringify({
				"id":uuidv4(), "receiver":uNamee, "adName":adIdd,"sender":mojKorisnik.username, "msgTitle":naslovMsg,
				"msgContent":opisMsg, "dateAndTime":datumm.getTime(), "read":false, "deleted":false
			}),
			success:function(){
				alert('Message sent');
				window.location.reload();
			},
			error:function(){
				alert('Error occured');
			}
		});
		 
	 });
	 
	 //EDITUJ PORUKU NA CLICK
	 $('#modal_confirm_edit').click(function(){
			
			if(document.getElementById('select_ad_edit').value==''){
				alert('All fields must be filled');
				return;
			}
			if($('#message_title_edit').val()==''){
				alert('All fields must be filled');
				return;
			}
			if($('#message_description_edit').val()==''){
				alert('All fields must be filled');
				return;
			}
			
			let adIdd = $('#select_ad_edit option:selected').val();
			let naslovMsg = $('#message_title_edit').val();
			let opisMsg = $('#message_description_edit').val();
			
					
			$.post({
				url:'rest/message/edit/'+adIdd+'/'+naslovMsg+'/'+opisMsg+'/'+mojaPor.id,
				success:function(){
					alert('Message edited');
					window.location.reload();
				},
				error:function(){
					alert('Error occured');
				}
			});
			 
		 });
	 
	 
	//KAD IZADJE IZ MODALA DA MENJA USERROLE DA ANULIRA SVE SELECTED
	 $('#modal_cancel').click(function(){
		 $('#inlineRadioBuyer').prop("checked", false);
		 $('#inlineRadioAdmin').prop("checked", false);
		 $('#inlineRadioSeller').prop("checked", false);
	    document.getElementById('select_user').value ="";
	 });
	 $('#modal_close').click(function(){
		 $('#inlineRadioBuyer').prop("checked", false);
		 $('#inlineRadioAdmin').prop("checked", false);
		 $('#inlineRadioSeller').prop("checked", false);
	    document.getElementById('select_user').value ="";
	 });
	 
	 
	 //KAD POTVRDI MODAL CHANGE USER ROLE DA MU PREUZME PODATKE I SACUVA IH
	 $('#modal_confirm').click(function(){
		if(document.getElementById('select_user').value ==""){
			alert('All fields must be filled');
			return;
		}
		
		let uName = $('#select_user option:selected').text();
		let uRole = -1;
		
		if($('#inlineRadioBuyer').prop("checked")==true){
			uRole=0;
		}
		else if($('#inlineRadioAdmin').prop("checked")==true){
			uRole=1;
		}
		else if($('#inlineRadioSeller').prop("checked")==true){
			uRole=2;
		}
		
		console.log(uName+"    " +uRole);
		
		$.post({
			url: 'rest/changeRole/'+uName+'/'+uRole,
			contentType: 'application/json',
			success:function(){
				window.location='./webshop.html';
			}
		});
		 
	 });
	 
	 //KAD POTVRDI MODAL RESET WARNINGS
	 $('#modal_confirmReset').click(function(){
		if(document.getElementById('select_userReset').value==""){
			alert('Seller must be selected');
			return;
		}
		
		let uuName = $('#select_userReset option:selected').text();
		
		$.post({
			url: 'rest/resetWarning/'+uuName,
			contentType: 'application/json',
			success:function(){
				window.location='./webshop.html';
			}
		});
		 
	 });
	 
	 
	 
	 //KLIK NA LUPU IZBACI ALERT ZA SADA
	//$('#btn_user_search').click(function(){
		//alert('Klikne ga');
	//});
		
	//ENTER U SEARCH POLJU IZBACI ALERT ZA SADA
	//var mojInput = document.getElementById('user_search');

	// Execute a function when the user releases a key on the keyboard
	//mojInput.addEventListener("keyup", function(event) {
	  // Number 13 is the "Enter" key on the keyboard
	  //if (event.keyCode === 13) {
	    // Cancel the default action, if needed
	    //event.preventDefault();
	    // Trigger the button element with a click
	    //document.getElementById("myBtn").click();
	    //alert('Udari enter');
	// }
	//});
	
	
	//KLIK NA HREF CREATE AD DA SAKRIJE SVE OGLASE I DA PRIKAZE PRAVLJENJE NOVOG
	$("#href_create_ad").click(function(e){
		e.preventDefault();
		
		$("#advertisementItems").attr("hidden",true);
		$("#addAdvertisement").attr("hidden",false);
		$("#myMessages").attr("hidden",true);
		$('#myInbox').attr("hidden",true);
		$("#advertisementItemsBuyer").attr("hidden",true);
		$('#postedRecensions').attr("hidden",true);
		$('#addRecension').attr("hidden",true);
		$('#editRecension').attr("hidden",true);






	});
	
	$("#btn_cancel_ad").click(function(){
				
		document.getElementById("ad_name").value='';
		document.getElementById("ad_price").value='';
		document.getElementById("ad_description").value='';
		document.getElementById("ad_path").value='Image*';
		document.getElementById("ad_city").value='';
		
		$("#addAdvertisement").attr("hidden",true);
		$("#advertisementItems").attr("hidden",false);
		$("#myMessages").attr("hidden",true);
		$('#myInbox').attr("hidden",true);
		$("#advertisementItemsBuyer").attr("hidden",true);
		$('#postedRecensions').attr("hidden",true);
		$('#addRecension').attr("hidden",true);
		$('#editRecension').attr("hidden",true);
		$('#editAd').attr("hidden",true);
		$('#editCategory').attr("hidden",true);
		$('#editCategory').attr("hidden",true);








	});
	
	//FORMA ZA DODAVANJE OGLASA
	$("#forma").submit(function(event){
		event.preventDefault();
		

		let imeOglas =$("#ad_name").val();
		let cenaOglas = $("#ad_price").val();
		let opisOglas = $("#ad_description").val();
		let slikaOglas = imgInBase64;
		let gradOglas = $("#ad_city").val();
		
		var date=new Date();
				
		if(imeOglas== null || imeOglas==undefined || imeOglas=="" || cenaOglas==null || cenaOglas==undefined || cenaOglas=="" || opisOglas==null || opisOglas==undefined || opisOglas=="" ||
				slikaOglas==null || slikaOglas==undefined || slikaOglas=="" || gradOglas==null || gradOglas==undefined || gradOglas==""){
			alert('All fields must be filled to create new ad');
			return;
		}
		
		console.log("Dodao oglas sa imenom: "+imeOglas);
		
		if(mojKorisnik.reportSeller>3){
			alert("Account is banned for false advertising, therefore unable to post new ads");
			return;
		}
		
		$.post({
			url:'rest/ads/postAd',
			data: JSON.stringify({"id":uuidv4(), "name":imeOglas, "price":cenaOglas, "description":opisOglas,
				"numLikes":0, "numDislikes":0, "imgPath":slikaOglas, "datePublished":date.getTime(),
				"dateExpired":date.getTime()+2592000000, "active":true, "recensions":null, "city":gradOglas
				
			}),
			contentType: 'application/json',
			success:function(){
				alert('DODAO OGLAS');
				window.location='./webshop.html';
			},
			error:function(){
				alert('GRESKA');
			}
		});
	});
	
	//FORMA ZA IZMENU OGLASA
	$('#formaEditAd').submit(function(e){
		e.preventDefault();
		
		
		let slikaPromena="NemaNoveSlike";
		if($('#ad_pathEdit').val()!=null && $('#ad_pathEdit').val()!="Image*" && $('#ad_pathEdit').val()!=undefined){
			slikaPromena = imgInBase642;
		}
		
		let imeOglas =$("#ad_nameEdit").val();
		let cenaOglas = $("#ad_priceEdit").val();
		let opisOglas = $("#ad_descriptionEdit").val();
		let gradOglas = $("#ad_cityEdit").val();
		
		if(imeOglas==null || imeOglas==undefined || imeOglas==""){
			alert("All fields must be filled");
			return;
		}
		if(cenaOglas==null || cenaOglas==undefined || cenaOglas==""){
			alert("All fields must be filled");
			return;
		}
		if(opisOglas==null || opisOglas==undefined || opisOglas==""){
			alert("All fields must be filled");
			return;
		}
		if(gradOglas==null || gradOglas==undefined || gradOglas==""){
			alert("All fields must be filled");
			return;
		}
		
		if(mojKorisnik.reportSeller>3){
			alert("Account is banned for false advertising, therefore unable to edit ads");
			return;
		}
		
		$.post({
			url:'rest/ads/editAd/'+mojIdOgl+'/'+imeOglas+'/'+cenaOglas+'/'+opisOglas+'/'+gradOglas,
			contentType: 'application/json',
			data:JSON.stringify({slikaPromena}),
			success:function(){
				alert('IZMENIO OGLAS');
				window.location.reload();
			},
			error:function(){
				alert('GRESKA');
			}
		});
		
	});
	
	
	
	$('button#odjava').click(function(){
		event.preventDefault();
		
		$.post({
			url:'rest/logout',
			success: function(product) {
				alert('Uspesno logoutovanje');
				window.location='./login.html';
			}
		});
	});
	
	$('button#dodaj').click(function() {
		$('form#forma').show();
	});
	
	$('form#forma').submit(function(event) {
		event.preventDefault();
		let name = $('input[name="name"]').val();
		let price = $('input[name="price"]').val();
		if (!price || isNaN(price)) {
			$('#error').text('Cena mora biti broj!');
			$("#error").show().delay(3000).fadeOut();
			return;
		}
		$('p#error').hide();
		$.post({
			url: 'rest/products',
			data: JSON.stringify({id: '', name: name, price: price}),
			contentType: 'application/json',
			success: function(product) {
				$('#success').text('Novi proizvod uspešno kreiran.');
				$("#success").show().delay(3000).fadeOut();
				// Dodaj novi proizvod u tabelu
				addProductTr(product);
			}
		});
	});
});