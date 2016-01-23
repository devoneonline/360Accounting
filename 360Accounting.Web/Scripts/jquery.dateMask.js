/**
*	Date Mask and Validate
*
*	Description: Esse é um plugin para o jQuery que tem a finalidade de transformar um input text para aceitar apenas datas. Para isso, ele cria uma máscara no campo e corrige automaticamente erros de digitação.
*		Seus parâmetros de configuração são: 
*			* separator - Caracter que separa o dia, mês ou ano. Podendo ser \/_*., -
*			* format - Formato da data. Podendo ser a combinação emtre D, M e Y.
*				Ex.: "DMY", "DM", "MY", "YMD".
*			* minYear - Menor ano aceito;
*			* maxYear - Maior ano aceito;
*		Exemplo de utilização:
*			Simples:
*				jQuery(function($){
*					$(seletor).dateMask();
*				});
*			Completo:
*				jQuery(function($){
*					$(seletor).dateMask({separator:'/', format:'dmy', minYear:1900, maxYear:2011});
*				});
*	Author: Anderson Nogueira
*	http://nopogs.blogspot.com/2011/03/plugin-jquery-date-mask.html
*	Version: 1.0.1 (14/06/2011)
*/
(function($) {
	var pasteVar = ($.browser.msie ? 'paste' : 'input') + ".dateMask";
	
	$.fn.extend({
		dateMask: function(settings) {

			//CONFIGURAÇÕES DO PLUGIN
			settings = $.extend({
				separator: "/", //CARACTER QUE SEPARA O DIA DO MÊS DO ANO
				format: 'mdy', //FORMATO PARA A DATA ONDE "D" REPRESENTA DIA, "M" REPRESENTA MÊS E "Y" REPRESENTA ANO
				minYear: 1,
				maxYear: 9999				
			}, settings);
			
			//VALIDAR PARÂMETROS DE CONFIGURAÇÃO
			if(/^[\\\/_\* \.,-]$/.exec(settings.separator) == null){
				alert('O parâmetro separador está incorreto.\r\nOs caracteres válidos são: "\/_* .,-"');
				return;
			}
			if(/[^m|d|y]/i.exec(settings.format) != null || settings.format.length > 3){
				alert('O parâmetro format está incorreto.\r\nOs caracteres válidos são: d, m ou y.\r\nEx.: "dmy"');
				return;
			}
		
			return this.each(function() {
				var input = $(this);
				
				function keydownFunc(e) {
					var k = e.keyCode;
					ignore = (k < 16 || (k > 16 && k < 32) || (k > 32 && k < 41) || k == 46 /*DEL*/);
					if(input.val() == getFormat()){
						input.val('');
					}
				};

				function keypressFunc(e) {
					if (ignore) {
						ignore = false;						
						return true;
					}
					e = e || window.event;
					var k = e.charCode || e.keyCode || e.which;

					if (e.ctrlKey || e.altKey || e.metaKey) {//Ignore
						return true;
					} else if ((k >= 32 && k <= 125) || k > 186) {//typeable characters
						var padrao = /[0-9]/;
						var valor = input.val();
						var caract = String.fromCharCode(k);
						if(valor.length == getFormat().length){ //SIMULA O MAX LENGTH
							return false;
						}
						if(padrao.exec(caract)){
							return true;
						}
					}
					return false;
				};
				
				function keyupFunc(e) {				
					formatDate();
				};
				
				function getFormat() {
					var result = '';
					for(var i = 0; i < settings.format.length; i++){
						switch(settings.format.charAt(i).toLowerCase()){
							case 'm':
								result += 'mm' + settings.separator;
							break;
							case 'd':
								result += 'dd' + settings.separator;
							break;
							case 'y':
								result += 'yyyy' + settings.separator;
							break;
						}
					}
					result = result.substr(0, result.length - 1);
					return result;
				}
				
				function formatDate(){
					var re = new RegExp('\\' + settings.separator, 'g');
					var value = input.val().replace(re, '');
					var result = '';
					var j = 0;
					var d = '', m = '', y = '';
					var completeBlock = false;
					
					for(var i = 0; i < value.length; i++){			
						
						completeBlock = (getFormat().charAt(i+j) == settings.separator || i == value.length - 1);
						
						if(getFormat().charAt(i+j) == settings.separator){
							result += getFormat().charAt(i+j);
							j++;
						}
						result += value.charAt(i);

						switch(getFormat().charAt(i+j)){
							case 'm':
								m += value.charAt(i);
							break;
							case 'd':
								d += value.charAt(i);
							break;
							case 'y':
								y += value.charAt(i);
							break;
						}
						
						if(completeBlock){
							
							//VALIDANDO O ANO
							if(y.length == 4){
								if(y < settings.minYear) y = settings.minYear;								
								else if (y > settings.maxYear) y = settings.maxYear;
								
								while((''+y).length < 4){
									y = '0' + y;
								}
								
								result = replaceBlock('y', result, y);
							}
							
							//VALIDANDO O MÊS
							if(m.length == 2){
								if(m < 1) m = '01';
								else if (m > 12) m = 12;
								result = replaceBlock('m', result, m);
							}
							
							//VALIDANDO O DIA
							if(d.length == 2 && m.length == 2){
								if(d < 1) d = '01';						
								if(isMonth31(m)){
									if(d > 31) d = 31;
									
								}else if(m == 2){
									if(y.length == 4){
										if ((y % 4 == 0) && ((y % 100 != 0) || (y % 400 == 0))){
											if(d > 29) d = 29;
										}else{
											if(d > 28) d = 28;
										}										
									}else if(d > 29){
										d = 29;
									}									
								}else{
									if( d > 30) d = 30;
								}								
							}else if(d > 31){
								d = 31;
							}							
							result = replaceBlock('d', result, d);
						}						
						completeBlock = false;
					}					
					input.val(result);
				}
				
				function replaceBlock(index, search, replace){
					var ini = getFormat().indexOf(index);
					var end = getFormat().lastIndexOf(index);
					
					return search.substring(0, ini) + replace + search.substring(end + 1);
				}
				
				function isMonth31(month) {					
					return /01|03|05|07|08|10|12/.exec(month) != null;
				}
				
				function focusFunc(e) {
					if(input.val() == ""){
						input.val(getFormat());
					}
				};
				
				function blurFunc(e) {
					if(input.val().length != getFormat().length || input.val() == getFormat()){
						input.val('');
					}
				};
				
				function pasteFunc() {
					var val = input.val();
					input.val(val.replace(/[^0-9]/g, ''));
					formatDate();
				}

				if (!input.attr("readonly"))
					input
					.bind("keydown.dateMask", keydownFunc)
					.bind("keypress.dateMask", keypressFunc)
					.bind("keyup.dateMask", keyupFunc)
					.bind("blur.dateMask", blurFunc)
					.bind("focus.dateMask", focusFunc)
					.bind(pasteVar, pasteFunc);
			});
		}
	});
})(jQuery);