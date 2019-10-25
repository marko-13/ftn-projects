//PROVERA DA LI JE FOTOGRAFIJA AKO NIJE RESETUJ POLJE I IZBACI ALERT
function validateFileType(){
			var fileName=document.getElementById("ad_img").value;
			var idxDot = fileName.lastIndexOf(".")+1;
			var extFile = fileName.substr(idxDot, fileName.length).toLowerCase();
			if(extFile=="jpg" || extFile=="jpeg" || extFile=="png"){
				alert("Dobra ekstenzija fajla, dobar tip fajla");
			}
			else{
				alert("Only jpg/jpeg and png files are allowed!");
				document.getElementById("ad_img").value='';
			}
		}


window.onload=function(){
	 alert("The URL of this page is: " + window.location.href);
}
//***********DOCUMENT READY
$(document).ready(function() {
	
	//SIDEBAR
	 $('#sidebarCollapse').on('click', function () {
	        $('#sidebar').toggleClass('active');
	        
	        //var boja=$("#ad_description").css("opacity");
	        //console.log(boja);
	    });
	
	
	
	
	
	$('#forma').submit(function(event){
		event.preventDefault();
		
		let ad_naziv=$('#ad_name').val();
		let ad_cena=$('#ad_price').val();
		let ad_opis = $('#ad_description').val();
		let ad_putanjaSlike = $('#ad_img').val();
		console.log(ad_putanjaSlike);
		let ad_grad = $('#ad_city').val();
		
		
		
		
	
	});
	
	
});