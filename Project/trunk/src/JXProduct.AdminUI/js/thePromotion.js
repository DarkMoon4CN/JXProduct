$(function(){


		$('.dataTile a,.promoXingQiTab .sz a:odd').on('click',function(){
			 $('.newPromot,.dataTile,.promoDateTable').hide();
			 $('.commAndInfoWrap').show();
		});
		$('.quxiaoBJ').on('click',function(){
			$('.newPromot,.dataTile,.promoDateTable').show();
			$('.commAndInfoWrap').hide();
		});
		$('.promotiConter .confirmBtn .q').on('click',function(){
				parent.layer.msg('保存成功',{time:1000});
		});
		$('.promotiConter .confirmBtn .x').on('click',function(){
			   $('.commAndInfoWrap').hide();
			    $('.newPromot,.dataTile,.promoDateTable').show();
		});
		$('.promoXingQiTab .sz a:even').on('click',function(){
				var _this = this;
				var o = parent.layer.confirm('确定要删除吗？',{icon: 1},function(){
					 parent.layer.close(o);
					 $(_this).parent().parent().parent().parent().parent().remove();
					parent.layer.msg('删除成功！',{time:1000});
				});
				
		});

	  $('.addBranBox').eq(0).find('li').hover(function(){
	  	    $(this).find('a').show();
	  },function(){
	  	     $(this).find('a').hide();
	  }).on('click','a',function(){
	  	   var _this = this;
	    		var o = parent.layer.confirm('确定要删除吗？',{icon: 1},function(){
	    			 parent.layer.close(o);
	    			 $(_this).parent().remove();
	    			parent.layer.msg('删除成功！',{time:1000});
	    		});
	  });
	  $('.addBranBox').eq(1).find('li').hover(function(){
	      	  $(this).find('.beiMian').stop().animate({
	      	  	top:-1
	      	  },200);
	  },function(){
	    	$(this).find('.beiMian').stop().animate({
	      	  	top:79
	      	  },200);
	  });

});