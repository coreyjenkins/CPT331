//
//  UIView.swift
//  CPT331.iOS
//
//  Created by Peter Weller on 7/10/2016.
//  Copyright © 2016 Peter Weller. All rights reserved.
//

import UIKit

extension UIView {

    /**
        Forces the view to match the frame size of its parent view by setting top, trailing, bottom & leading constraints.
     
        - Parameters:
            - view: the specified view must be the parent view
    */
    func sizeToFit(parent parentView:UIView) {
        self.translatesAutoresizingMaskIntoConstraints = false
        parentView.addConstraints([
            NSLayoutConstraint(item: self, attribute: .Top, relatedBy: .Equal, toItem: parentView, attribute: .Top, multiplier: 1, constant: 0),
            NSLayoutConstraint(item: self, attribute: .Trailing, relatedBy: .Equal, toItem: parentView, attribute: .Trailing, multiplier: 1, constant: 0),
            NSLayoutConstraint(item: self, attribute: .Bottom, relatedBy: .Equal, toItem: parentView, attribute: .Bottom, multiplier: 1, constant: 0),
            NSLayoutConstraint(item: self, attribute: .Leading, relatedBy: .Equal, toItem: parentView, attribute: .Leading, multiplier: 1, constant: 0)
        ])
    }
    
    
    /**
        Adds a loading indicator to the center of the view.
     
        - Parameters:
            - style: the style of the indicator view: .Gray, .White, .WhiteLarge
     
        - Returns: the loading indicator subview so that it can be later hidden by calling indicator.removeFromSuperView()
     */
    func showLoadingIndicator(style style:UIActivityIndicatorViewStyle = .Gray) -> UIActivityIndicatorView {
        let indicator = UIActivityIndicatorView(frame: self.bounds)
        indicator.activityIndicatorViewStyle = style
        indicator.autoresizingMask = [.FlexibleWidth, .FlexibleHeight]
        indicator.userInteractionEnabled = false
        
        self.addSubview(indicator)
        indicator.startAnimating()
        
        return indicator
    }
    
    
    /**
        Adds UILabel to the center of the view.
     
        - Parameters:
            - message: the message to be displayed, default: "Network Connection Required"
            - fontSize: label font size, default: 15
            - color: label color, default: dark grey
     
        - Returns: the label subview so that it can be later hidden by calling label.removeFromSuperView()
     */
    func showMessage(message:String, fontSize:CGFloat = 15, color:UIColor = UIColor(red: 0.1, green: 0.1, blue: 0.1, alpha: 0.7)) -> UILabel {
        let label = UILabel(frame: self.bounds)
        
        // Formatting
        label.text = message
        label.textColor = color
        label.textAlignment = .Center
        label.font = UIFont.systemFontOfSize(fontSize)
        
        label.autoresizingMask = [.FlexibleWidth, .FlexibleHeight]
        self.addSubview(label)
        
        return label
    }
    
    
    /**
        Adds a network required message to the center of the view.
     
        - Returns: the label subview so that it can be later hidden by calling label.removeFromSuperView()
     */
    func showNetworkMissingMessage(message:String="Network Connection Required", color:UIColor = UIColor(red: 0.1, green: 0.1, blue: 0.1, alpha: 0.7)) -> UILabel {
        return self.showMessage("Network Connection Required")
    }
}