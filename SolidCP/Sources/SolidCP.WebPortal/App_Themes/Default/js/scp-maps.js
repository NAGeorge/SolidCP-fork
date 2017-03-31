$(document).ready(function(){

	var mapColors = ['#9dc9cc', '#93b2b4', '#618d8f', '#276062', '#134a4d'];

	//*******************************************
	/*	MAP WITH ZOOM
	/********************************************/

	if($('.zoom-map').length > 0 ) {
		$mapZoom = $(".zoom-map");
		$mapZoom.mapael({
			map : {
				name: "france_departments",
				zoom: {
					enabled: true,
					maxLevel : 10
				}, 
				defaultPlot: {
					attrs: {
						opacity : 0.6
					}
				}
			},
			areas: {
				"department-56" : {
					text : {content : "56"}, 
					tooltip: {content : "Morbihan (56)"}
				}
			},
			plots : {
				'paris' : {
					latitude : 48.86, 
					longitude: 2.3444
				},
				'lyon' : {
					type: "circle",
					size:50,
					latitude :45.758888888889, 
					longitude: 4.8413888888889, 
					value : 700000, 
					href : "http://fr.wikipedia.org/wiki/Lyon",
					tooltip: {content : "<span style=\"font-weight:bold;\">City :</span> Lyon"},
					text : {content : "Lyon"}
				},
				'rennes' : {
					type :"square",
					size :20,
					latitude : 48.114166666667, 
					longitude: -1.6808333333333, 
					tooltip: {content : "<span style=\"font-weight:bold;\">City :</span> Rennes"},
					text : {content : "Rennes"},
					href : "http://fr.wikipedia.org/wiki/Rennes"
				}
			}
		});

		// Zoom on mousewheel with mousewheel jQuery plugin
		$mapZoom.on("mousewheel", function(e) {
			if (e.deltaY > 0)
				$mapZoom.trigger("zoom", $mapZoom.data("zoomLevel") + 1);
			else
				$mapZoom.trigger("zoom", $mapZoom.data("zoomLevel") - 1);
				
			return false;
		});

		// focus to paris
		$('#focus-paris').on('click', function() {
			// Translate latitude,longitude of Paris to x,y coordinates
			var coords = $.fn.mapael.maps["france_departments"].getCoords(48.114167, 2.3444);
			$mapZoom.trigger('zoom', [10, coords.x, coords.y]);
		});

		$('#focus-lyon').on('click', function() {
			// Translate latitude,longitude of Lyon to x,y coordinates
			var coords = $.fn.mapael.maps["france_departments"].getCoords(45.758888888889, 4.8413888888889);
			$mapZoom.trigger('zoom', [5, coords.x, coords.y]);
		});

		$('#map-clear-zoom').on('click', function() {
			$mapZoom.trigger('zoom', [0]);
		});
	}


	//*******************************************
	/*	MAP WITH CIRCLE PLOT
	/********************************************/

	if($('.data-us-map').length > 0) {
		$('.data-us-map').mapael({
			map: {
				name: "usa_states",
				defaultPlot: {
					size: 10
				},
				defaultArea: {
					attrs: {
						stroke: "#fafafa", 
						"stroke-width": 1,
						fill: "#c4c4c4"
					}
				}
			},
			legend: {
				plot: {
					display: true,
					title: "US Sales Map",
					hideElemsOnClick: {
						opacity : 0
					},
					slices: [ 
						{
							size: 10,
							type: "circle",
							max: 500,
							attrs: { fill: mapColors[3] },
							label: "Less than 500 sales"
						},
						{
							size: 20,
							type: "circle",
							min: 500,
							max: 750,
							attrs: { fill: mapColors[3] },
							label: "Between 500 and 750 sales"
						},
						{
							size: 30,
							type: "circle",
							min: 750,
							max: 1000,
							attrs: { fill: mapColors[3] },
							label: "Between 750 and 1000 sales"
						},
						{
							size: 40,
							type: "circle",
							min: 1000,
							max: 1250,
							attrs: { fill: mapColors[3] },
							label: "Between 1000 and 1250 sales"
						},
						{
							size: 50,
							type: "circle",
							min: 1250,
							max: 1500,
							attrs: { fill: mapColors[3] },
							label: "Between 1250 and 1500 sales"
						}
					]
				}
			},
			plots: {
				"ny": {
					value: 1450,
					latitude: 40.717079,
					longitude: -74.00116,
					tooltip: { content: "<span>New York</span><br />Sales: 1450" }
				},
				'fl': {
					value: 900,
					latitude: 30.4518, 
					longitude: -84.27277,
					tooltip: {content : "<span>Florida</span><br />Sales: 900"}
				},
				'sf': {
					value: 1200,
					latitude: 37.792032,
					longitude: -122.394613,
					tooltip: {content : "<span>San Francisco</span><br />Sales: 1200"}
				},
				'ky': {
					value: 400,
					latitude: 38.197274,
					longitude: -84.86311,
					tooltip: {content : "<span>Kentucky</span><br />Sales: 400"}
				},
				'nm': {
					value: 850,
					latitude: 35.667231,
					longitude: -105.964575,
					tooltip: {content : "<span>New Mexico</span><br />Sales: 850"}
				},
				'nv': {
					value: 30,
					latitude: 39.160949,
					longitude: -119.753877,
					tooltip: {content : "<span>Nevada</span><br />Sales: 30"}
				},
				'il': {
					value: 1100,
					latitude: 39.783250,
					longitude: -89.650373,
					tooltip: {content : "<span>Illinois</span><br />Sales: 1100"}
				},
				'co': {
					value: 70,
					latitude: 39.7391667,
					longitude: -104.984167,
					tooltip: {content : "<span>Colorado</span><br />Sales: 70"}
				}
			}
		}); // end map with circle plot
	}
	
	//*******************************************
	/*	MAP DATA VISUALIZATION
	/********************************************/

	// data source
	var data = {
		"areas" : {
			"US": {
				"value": 2200,
				"tooltip": {
					"content": "<span>United States</span><br />Sales: 2200"
				}
			},
			"CN": {
				"value": 1800,
				"tooltip": {
					"content": "<span>China</span><br />Sales: 1800"
				}
			},
			"JP": {
				"value": 1550,
				"tooltip": {
					"content": "<span>Japan</span><br />Sales: 1550"
				}
			},
			"IN": {
				"value": 1400,
				"tooltip": {
					"content": "<span>India</span><br />Sales: 1400"
				}
			},
			"DE": {
				"value": 1600,
				"tooltip": {
					"content": "<span>Germany</span><br />Sales: 1600"
				}
			},
			"RU": {
				"value": 900,
				"tooltip": {
					"content": "<span>Russia</span><br />Sales: 900"
				}
			},
			"GB": {
				"value": 1200,
				"tooltip": {
					"content": "<span>United Kingdom</span><br />Sales: 1200"
				}
			},
			"FR": {
				"value": 1100,
				"tooltip": {
					"content": "<span>France</span><br />Sales: 1100"
				}
			},
			"BR": {
				"value": 400,
				"tooltip": {
					"content": "<span>Brazil</span><br />Sales: 400"
				}
			},
			"IT": {
				"value": 700,
				"tooltip": {
					"content": "<span>Italy</span><br />Sales: 700"
				}
			},
			"MX": {
				"value": 1900,
				"tooltip": {
					"content": "<span>Mexico</span><br />Sales: 1900"
				}
			},
			"ES": {
				"value": 300,
				"tooltip": {
					"content": "<span>Spain</span><br />Sales: 300"
				}
			},
			"KR": {
				"value": 200,
				"tooltip": {
					"content": "<span>South Korea</span><br />Sales: 200"
				}
			},
			"CA": {
				"value": 2900,
				"tooltip": {
					"content": "<span>Canada</span><br />Sales: 2900"
				}
			},
			"ID": {
				"value": 1200,
				"tooltip": {
					"content": "<span>Indonesia</span><br />Sales: 1300"
				}
			},
			"TR": {
				"value": 90,
				"tooltip": {
					"content": "<span>Turkey</span><br />Sales: 90"
				}
			},
			"IR": {
				"value": 80,
				"tooltip": {
					"content": "<span>Iran</span><br />Sales: 80"
				}
			},
			"AU": {
				"value": 900,
				"tooltip": {
					"content": "<span>Australia</span><br />Sales: 1400"
				}
			},
			"ZA": {
				"value": 50,
				"tooltip": {
					"content": "<span>South Africa</span><br />Sales: 50"
				}
			},
			"EG": {
				"value": 20,
				"tooltip": {
					"content": "<span>Egypt</span><br />Sales: 20"
				}
			},
			"PK": {
				"value": 1300,
				"tooltip": {
					"content": "<span>Pakistan</span><br />Sales: 1300"
				}
			},
			"SG": {
				"value": 100,
				"tooltip": {
					"content": "<span>Singapore</span><br />Sales: 100"
				}
			},
		}
	} // end data source

	// map with data visualization
	if($('.data-visualization-map').length > 0 ) {
		$('.data-visualization-map').mapael({
			map: {
				name: "world_countries",
				defaultArea: {
					attrs: {
						stroke : "#fff", 
						"stroke-width" : 0.1,
						fill: "#A8A5A5"
					},
					attrsHover: {
						fill: "#c4c4c4"
					},
				}
			},
			legend: {
				area: {
					display: true,
					title: "Sales Number",
					labelAttrs: {"font-size" : 12},
					marginLeft: 5,
					marginBottom: 8,
					slices: [
						{
							max: 100,
							attrs: {
								fill: mapColors[0]
							},
							label: "< 100"
						},
						{
							min: 100,
							max: 500,
							attrs: {
								fill: mapColors[1]
							},
							label: "100 - 500"
						},
						{
							min: 500,
							max: 1000,
							attrs: {
								fill: mapColors[2]
							},
							label: "500 - 1000"
						},
						{
							min: 1000,
							max: 1500,
							attrs: {
								fill: mapColors[3]
							},
							label: "1000 - 1500"
						},
						{
							min: 1500,
							attrs: {
								fill: mapColors[4]
							},
							label: "> 1500"
						}
					]
				}
			},
			areas: data['areas']

		}); // end data visualization map
	}

});