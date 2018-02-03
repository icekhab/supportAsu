$(document).ready(function () {
	 var block = document.getElementById("scroll");
	 block.scrollTop = block.scrollHeight;
	 $('#scroll').resize(()=>{
		 var block = document.getElementById("scroll");
	     block.scrollTop = block.scrollHeight;
	 });
})
function OnBegin()
{
    $('#Text').val('');
}
function OnSuccess(data)
{
    $('.claim_comments').append(data);
    $('#scroll').resize();
}