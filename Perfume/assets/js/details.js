let count = 1;
            const decrease = document.querySelector(".decrease")
            const increase = document.querySelector(".increase")
            const value = document.querySelector(".number")
            increase.addEventListener("click",function()
        {
            count++;
            number.value = count;
            
        })
        decrease.addEventListener("click",function()
        {       
            if(number.value <= 1)
                number.value = 1;
            else
            {
                count--;
                
            }
            number.value = count;     
        })

            var ProductImg = document.getElementById("ProductImg");
            var SmallImg = document.getElementsByClassName("small-img");
            SmallImg[0].onclick = function()
            {
                ProductImg.src = SmallImg[0].src;
            }
            SmallImg[1].onclick = function()
            {
                ProductImg.src = SmallImg[1].src;
                
            }
            SmallImg[2].onclick = function()
            {
                ProductImg.src = SmallImg[2].src;
                
            }
            SmallImg[3].onclick = function()
            {
                ProductImg.src = SmallImg[3].src;
                
            }