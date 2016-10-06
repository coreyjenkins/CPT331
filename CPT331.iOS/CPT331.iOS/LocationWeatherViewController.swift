//
//  LocationInfoViewController.swift
//  CPT331.iOS
//
//  Created by Peter Weller on 20/09/2016.
//  Copyright © 2016 Peter Weller. All rights reserved.
//

import UIKit

class LocationWeatherViewController: LocationSubViewController {

    
    @IBOutlet weak var currentTemperatureLabel: UILabel!
    @IBOutlet weak var currentCategoryImageView: UIImageView!
    @IBOutlet weak var currentCategoryNameLabel: UILabel!
    
    @IBOutlet weak var humidityView: WeatherStatView!
    @IBOutlet weak var windSpeedView: WeatherStatView!
    @IBOutlet weak var windBearingView: WeatherStatView!
    
    // TODO: add support for dynamically adding prediction views
    @IBOutlet weak var predictionView1: WeatherPredictionView!
    @IBOutlet weak var predictionView2: WeatherPredictionView!
    @IBOutlet weak var predictionView3: WeatherPredictionView!
    @IBOutlet weak var predictionView4: WeatherPredictionView!
    var predictionViews:[WeatherPredictionView]?
    
    override func viewDidLoad() {
        super.viewDidLoad()

        self.humidityView.type = .Humidity
        self.windSpeedView.type = .WindSpeed
        self.windBearingView.type = .WindBearing
        
        self.currentTemperatureLabel.text = "??"
        self.currentCategoryNameLabel.text = ""
        
        WeatherManager.getWeather(atCoordinate: self.location.coordinate) { data in
            self.showWeatherData(data)
        }
        
        self.predictionViews = [
            self.predictionView1,
            self.predictionView2,
            self.predictionView3,
            self.predictionView4
        ]
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    func showWeatherData(data: WeatherDataCollection?) {
        
        
        if let current = data?.current {
            
            // Top view
            if let temperature = current.temperature {
                self.currentTemperatureLabel.text = String(Int(temperature))
            }
            
            if let category = current.category {
                self.currentCategoryNameLabel.text = category.name
                self.currentCategoryImageView.image = category.image
            }
            
            // Middle views
            if let humidity = current.humidity {
                self.humidityView.valueLabel.text = String(Int(humidity * 100))
            } else {
                self.humidityView.valueLabel.text = "?"
            }
            
            if let windSpeed = current.wind.speed {
                self.windSpeedView.valueLabel.text = String(Int(windSpeed))
            } else {
                self.windSpeedView.valueLabel.text = "?"
            }
            
            if let windBearing = current.wind.bearing {
                self.windBearingView.valueLabel.text = String(Double(windBearing).toWindDirectionFromDegrees())
                self.windBearingView.imageView.rotateImage(CGFloat(windBearing), flip: false)
            } else {
                self.windBearingView.valueLabel.text = "?"
            }
        }
        
        // TODO: predictions views should be dynamically added to parent view in the event of <4 predictions
        if let predictions = data?.dailyData {
            
            for i in 0..<predictions.count {
                if i < predictionViews?.count {
                    self.predictionViews![i].prediction = predictions[i]
                    
                } else {
                    break
                }
            }
        }
    }
}