var RecentlyView=function(e){var o=this;o.option=$.extend({debug:!1,data_selector:"[data-recently-view]",clear_forage:!1,use_forage:!0,del_current_id:!0,reverse:!1,productIds:[],keyParameters:"recently_view",success:function(){}},e),o.setLog("Настройки плагина",o.option),o.init()};RecentlyView.prototype.init=function(){var e=this;e.option.clear_forage&&localforage&&localforage.removeItem(e.option.keyParameters,function(){e.setLog("Локальное хранилище очищено","Ключ: "+e.option.keyParameters)}),void 0===window.localforage&&console.warn("Не подключен плагин localforage!"),e.getProducts().done(function(o){e.option.del_current_id?e.getIds(function(t){t&&o[t]&&(delete o[t],e.setLog("Из списка удален товар с id: "+t));for(var r=[],n=0;n<e.option.productIds.length;n++){var i=e.option.productIds[n];o[i]&&r.push(o[i])}e.setLog("Вызов колбека success"),e.option.success(r)}):(e.setLog("Вызов колбека success"),e.option.success(o),e.getIds())}).fail(function(o){e.setLog("Не удалось получить данные",o),e.getIds()})},RecentlyView.prototype.getIds=function(e){var o=this,t=o.option.data_selector.replace(/(?:\[data-*)*\]*/g,"");$(o.option.data_selector).each(function(e,r){o.option.productIds.unshift($(r).data(t).toString())}),o.setLocalData(o.unique(o.option.productIds));var r=o.option.productIds;(o.option.reverse&&(r=r.reverse()),o.option.productIds=o.unique(r),e)&&e($(o.option.data_selector+":first").data(t))},RecentlyView.prototype.unique=function(e){for(var o=[],t=0;t<e.length;t++)-1==o.indexOf(e[t])&&o.push(e[t]);return o},RecentlyView.prototype.getProducts=function(){var e=this;return $.when(function(){var o=jQuery.Deferred();window.localforage&&e.option.use_forage?e.getLocalData().done(function(t){e.option.productIds=t,$.each(t,function(e,o){0==o&&t.splice(e,1)}),t.length?$.post("/products_by_id/"+t.join(",")+".json").done(function(t){var r=t.products,n={};$.each(r,function(o,t){n[t.id]=e.convertProperties(t)}),e.setLog("Товары из апи: ",n),o.resolve(n)}).fail(function(e){o.resolve({})}):o.resolve({})}).fail(function(){o.resolve({})}):o.resolve({});return o.promise()}())},RecentlyView.prototype.getLocalData=function(){var e,o=this;return $.when((e=jQuery.Deferred(),localforage.getItem(o.option.keyParameters,function(t,r){r?(o.setLog("Данные получены из хранилища",r),e.resolve(r)):(o.setLog("Хранилище пусто, данные будут запрошены в kladr.insales.ru"),e.reject("Хранилище пусто"))}),e.promise()))},RecentlyView.prototype.setLocalData=function(e,o){var t=this,r=o||function(){};window.localforage&&t.option.use_forage&&localforage.setItem(t.option.keyParameters,e,function(e,o){o?(t.setLog("В хранилище обновлены данные через метод setLocalData",o),r(o)):t.setLog("Не удалось обновить данные")})},RecentlyView.prototype.convertProperties=function(e){function o(e,o,t){e[o]||(e[o]=t)}return e.parameters={},e.sale=null,$.each(e.properties,function(t,r){$.each(e.characteristics,function(t,n){if(r.id===n.property_id){e.property=r,o(e.parameters,r.permalink,r),o(e.parameters[r.permalink],"characteristics",[]);var i=!0;$.each(e.parameters[r.permalink].characteristics,function(e,o){o.id==n.id&&(i=!1)}),i&&e.parameters[r.permalink].characteristics.push(n)}})}),e.variants&&$.each(e.variants,function(o,t){if(t.old_price){var r=Math.round((parseInt(t.old_price)-parseInt(t.price))/parseInt(t.old_price)*100,0);r<100&&(e.sale=r)}}),e},RecentlyView.prototype.setLog=function(e,o){this.option.debug&&(console.info("==RecentlyView=="),console.log(e),o&&console.log(o))};
var myRecentlyView = new RecentlyView({
	success: function(_products){
		if(_products.length > 0){
			$('.js-owl-carousel-products-recently-slider').html(templateLodashRender(_products, 'product-card-recently'));
			$('#insales-section-products--recently').removeClass('d-none');
			$('.js-owl-carousel-products-recently-slider').owlCarousel({
				items:2,
				margin:20,
				loop:false,
				nav:false,
				navText:['<i class="far fa-chevron-left fa-lg"></i>','<i class="far fa-chevron-right fa-lg"></i>'],
				dots:true,
				responsive:{
					0:{
						items:2,
						nav:false,
						dots:true
					},
					768:{
						items:3,
						nav:false,
						dots:true
					},
					992:{
						items:4,
						nav:true,
						dots:false
					}
				}
			});
			Compare.update();
			if(InsalesThemeSettings.module_favorites){
				Favorite.checkFavoritesProducts();
			}
		}else{
			$('#insales-section-products--recently').addClass('d-none');
		}
	}
});
$('.js-recently-clear').on('click', function(e){
	e.preventDefault();
	$('#insales-section-products--recently').slideUp(400, function(){
		$('#insales-section-products--recently').addClass('d-none');
		if(localforage){
			localforage.removeItem('recently_view');
		}
	});
});
