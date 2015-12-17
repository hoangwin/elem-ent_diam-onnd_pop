
#include "ActivityIndicator.h"
#include "OrientationSupport.h"

#if !UNITY_TVOS
@interface ActivityIndicator : UIActivityIndicatorView
{
	UIView*	_parent;
}
@end
static ActivityIndicator* _activityIndicator = nil;


@implementation ActivityIndicator
- (void)show:(UIView*)parent
{
	_parent = parent;
	[parent addSubview: self];
	[self startAnimating];
}
- (void)layoutSubviews
{
	self.center = CGPointMake([_parent bounds].size.width/2, [_parent bounds].size.height/2);
}
@end
#endif // !UNITY_TVOS

void ShowActivityIndicator(UIView* parent, int style)
{
#if !UNITY_TVOS
	if(_activityIndicator != nil)
		return;

	if(style >= 0)
	{
		_activityIndicator = [[ActivityIndicator alloc] initWithActivityIndicatorStyle:(UIActivityIndicatorViewStyle)style];
		_activityIndicator.contentScaleFactor = [UIScreen mainScreen].scale;
	}

	if(_activityIndicator != nil)
		[_activityIndicator show:parent];
#endif
}

void ShowActivityIndicator(UIView* parent)
{
#if !UNITY_TVOS
	ShowActivityIndicator(parent, UnityGetShowActivityIndicatorOnLoading());
#endif
}

void HideActivityIndicator()
{
#if !UNITY_TVOS
	if(_activityIndicator)
	{
		[_activityIndicator stopAnimating];
		[_activityIndicator removeFromSuperview];
		_activityIndicator = nil;
	}
#endif
}

extern "C" void UnityStartActivityIndicator()
{
#if !UNITY_TVOS
    // AppleTV does not support activity indicators
	ShowActivityIndicator(UnityGetGLView());
#endif
}

extern "C" void UnityStopActivityIndicator()
{
#if !UNITY_TVOS
	HideActivityIndicator();
#endif
}
