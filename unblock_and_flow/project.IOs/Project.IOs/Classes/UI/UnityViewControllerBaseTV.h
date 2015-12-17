#pragma once

#if UNITY_TVOS

#import <UIKit/UIKit.h>

@class UnityView;

@interface UnityViewControllerBase : UIViewController
{
}
@end

// this is default view controller implementation
//
@interface UnityDefaultTVViewController : UnityViewControllerBase
{
}
@end

#endif
