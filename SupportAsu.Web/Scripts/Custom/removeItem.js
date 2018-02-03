$(document).delegate('.remove-pop-up-btn','click',function(event){
	var itemContent = $(this).closest('li'); 
	var itemId = $(itemContent).attr('data-id');
	var itemSrc = $(itemContent).attr('data-src');
	showRemovePopup( itemId, itemSrc);
});

$(document).delegate('.remove-item-btn','click',function(event){
	var itemId = $(this).attr('data-remove-id');
	var itemSrc = $(this).attr('data-remove-src');
	removeItem(itemId, itemSrc);
});

$(document).delegate('.close-small-pop-up-btn','click',function(event){
	$(this).closest('.modal').remove();
});

function showRemovePopup(itemId, itemSrc){
	html = `<div class="modal" style="display:block">
		<div class="small-pop-up">
			<span>Ви дійсно хочете видалити?</span>
			<button class="btn--red remove-item-btn" data-remove-id=` + itemId + ` data-remove-src = ` + itemSrc + `>Так</button>
			<button class="btn--green close-small-pop-up-btn">Ні</button>
		</div>
	</div>`
	$('body').append(html);
}
function removeItem(itemId, itemSrc) {
    startLoader();
	$.ajax({
			  type: "POST",
              url:itemSrc,
              data:{'id':itemId },
              success:function(r){
                  $(`[data-id = ` + itemId + `]`).remove();
                  $('.small-pop-up').closest('.modal').remove();
                  stopLoader();
              },
				error:function (thrownError){
					//выводим ошибку
				    alert('Помилка видалення');
				    stopLoader();
				}
        });
}