function progress()
{
				    var prg= document.getElementById('progress');
				    var progress=1;
				    var id= setInterval(frame,500);
				    var bool=0;
				    var x= document.getElementById('menu');
				    x.style.display='none';

				    function frame(){
				    	if(prg.value==100){
				    		if(bool==0){
					    		var x= document.getElementById('corps');
					    		x.style.display='none';
					    		//clearInterval(id);
					    		bool=1;
					    	}
				    		else{
				    			var y= document.getElementById('menu');
				    			y.style.display='block';
				    			clearInterval(id);
				    		}

				    	}
				    	else{
				    		
				    			progress=Math.random() * (10 - 1) + 1;
				    			prg.value+=progress;
				    		}
				    		
				    }
			
			}

		