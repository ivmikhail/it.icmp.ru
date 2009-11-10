
window.addEvent('domready', function() {

	
	var pager = $('pager');
	var pagerLinks = $$('#pager li');
	
	pager.fade('hide');
	pagerLinks.fade('hide');

	pagerLinks.set('tween', {duration: 200});
		
	pager.addEvent('mouseenter', function() { this.fade(1); });
	pagerLinks.addEvent('mouseleave', function() { this.fade(0.3); });
	
	pagerLinks.addEvent('mouseenter', function() { this.fade(1); });
	pagerLinks.addEvent('mouseleave', function() { this.fade(0.5); });
	
	(function() {
		var aCount = 0;
		pager.fade(1);
		pagerLinks.each(function(el) {
			(function() { el.fade(1).fade(0.5); }).delay(aCount * 100);
			aCount++;
		});
		(function() { $('pager').fade(0.3); }.delay(aCount * 100 + 100));
	}).delay(500);
});
