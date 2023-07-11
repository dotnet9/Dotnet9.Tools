jQuery(document).ready(function($){
  
	 var obj=null;
    var As=document.getElementById('starlist').getElementsByTagName('a');
    obj = As[0];
    for(i=1;i<As.length;i++){if(window.location.href.indexOf(As[i].href)>=0)
    obj=As[i];}
    obj.id='selected';		

	  //nav
    $("#mnavh").on('click', function (event) {
        $("#starlist").toggle();
	    $("#mnavh").toggleClass("open");
	    $("body").toggleClass("ovhi");
	});
	
	
	 $(window).scroll(function() {
        var h = $("body").height() - window.getHeight();
        //console.log(h);
        if ($(window).scrollTop() > 80 && h > 120) {
            $(".topnav").addClass("is-fixed").find("").fadeOut(400);			
		

        } else if ($(window).scrollTop() < 80) {
            $(".topnav").removeClass("is-fixed").find("").fadeIn(400);
		

        }
    });
	   //scroll to top
        var offset = 300,
        offset_opacity = 1200,
        scroll_top_duration = 700,
        $back_to_top = $('.icon-top');
		
    $(window).scroll(function () {
        ($(this).scrollTop() > offset) ? $back_to_top.addClass('cd-is-visible') : $back_to_top.removeClass('cd-is-visible cd-fade-out');
        if ($(this).scrollTop() > offset_opacity) {
            $back_to_top.addClass('cd-fade-out');
        }
    });
	 $back_to_top.on('click', function (event) {
        event.preventDefault();
        $('body,html').animate({
                scrollTop: 0,
            }, scroll_top_duration
        );
    });

	
});


   window.getHeight = function() {
    if (window.innerHeight != undefined) {
        return window.innerHeight;
    } else {
        var B = document.body
          , D = document.documentElement;
        return Math.min(D.clientHeight, B.clientHeight);
    }
}

window.windowInit = function () {
    $("#mnavh").on('click', function (event) {
        $("#starlist").toggle();
        $("#mnavh").toggleClass("open");
        $("body").toggleClass("ovhi");
    });
}

function handlePseudoLinks() {
    var pseudoLinks = document.querySelectorAll('a[href^="#"]');
    pseudoLinks.forEach(function (link) {
        var href = link.getAttribute('href');
        var absoluteUri = window.location.origin + window.location.pathname + href;
        link.setAttribute('href', absoluteUri);
    });
}

window.scrollToTop = function () {
    window.scrollTo({
        top: 0,
        behavior: 'smooth'
    });
}

window.pictureZoom = function () {
    // ֻ�������ڵ�img��ǩ��ӵ���Ŵ��¼�
    const div = document.getElementById('details-content');

    // ��ȡ���е�img��ǩ
    const imgs = div.querySelectorAll('img');

    // �������е�img��ǩ
    for (let i = 0; i < imgs.length; i++) {
        // ��ÿ��img��ǩ��ӵ���¼�
        imgs[i].addEventListener('click', function () {
            // ����һ���µ�div����
            const container = document.createElement('div');
            container.style.position = 'fixed';
            container.style.top = '0';
            container.style.left = '0';
            container.style.width = '100%';
            container.style.height = '100%';
            container.style.backgroundColor = 'rgba(0, 0, 0, 0.8)';
            container.style.zIndex = '9999';
            container.style.cursor = 'zoom-out';

            // ����һ���µ�img��ǩ
            const newImg = document.createElement('img');
            newImg.src = this.src;
            newImg.style.position = 'absolute';
            newImg.style.top = '50%';
            newImg.style.left = '50%';
            newImg.style.transform = 'translate(-50%, -50%)';
            newImg.style.maxWidth = '90%';
            newImg.style.maxHeight = '90%';
            newImg.style.objectFit = 'contain';

            // ����img��ǩ�Ͱ�ť��ӵ�������
            container.appendChild(newImg);

            // ��������ӵ�body��
            document.body.appendChild(container);

            let scale = 1; // ��ʼ�����ű���
            container.addEventListener('wheel', function (event) {
                event.preventDefault();
                const delta = event.deltaY > 0 ? 0.9 : 1.1;
                scale *= delta; // �ۻ����ű���
                newImg.style.transform = `scale(${scale})`;
            });

            // �������ʱ�Ƴ�����
            container.addEventListener('click', function () {
                document.body.removeChild(container);
            });
        });
    }
}