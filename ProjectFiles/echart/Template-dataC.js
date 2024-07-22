var dom = document.getElementById('containerC');
setInterval(function () { myChart.setOption(option); }, 10000);
var myChart = echarts.init(dom, null,
    {                                                                 // Initialize an ECharts chart
        renderer: 'canvas',                                                                                   // Use the canvas renderer
        useDirtyRect: false                                                                                   // Disable dirty rect optimization
    });
var app = {};                                                                                           // Create an empty JavaScript object

var option;

   
        option = {
            
            tooltip: {
                trigger: 'axis',
                axisPointer: {                                                                              // Configure the axis pointer for the tooltip
                    type: 'shadow'                                                                          // Use 'shadow' type for the axis pointer; it can also be 'line' or 'shadow'
                }
            },
            legend: {},
            grid: {
                left: '5%',
                right: '15%',
                bottom: '10%'
            },
            xAxis: {
                type: 'value'  
            },
            yAxis: {},
            toolbox: {
                right: 10,
                feature: {
                    dataZoom: {
                        yAxisIndex: 'none'
                    },
                    restore: {},
                    saveAsImage: {}
                }
            },
            dataZoom: [
                {
                    startValue: '2014-06-01'
                },
                {
                    type: 'inside'
                }
            ],
            visualMap: {
                top: 50,
                right: 10,
                pieces: [
                    {
                        gt: 0,
                        lte: 50,
                        color: '#93CE07'
                    },
                    {
                        gt: 50,
                        lte: 100,
                        color: '#FBDB0F'
                    },
                    {
                        gt: 100,
                        lte: 150,
                        color: '#FC7D02'
                    },
                    {
                        gt: 150,
                        lte: 200,
                        color: '#FD0100'
                    },
                    {
                        gt: 200,
                        lte: 300,
                        color: '#AA069F'
                    },
                    {
                        gt: 300,
                        color: '#AC3B2A'
                    }
                ],
                outOfRange: {
                    color: '#999'
                }
            },
            series: {
                name: 'Voltage',                                                                             // Series name
                type: 'line',                                                                                          // line chart type
                Refresh: 'Auto',                                                                                               // Stack bars in the 'total' group
                label: {                                                                                              // Configure labels for bars
                    show: true                                                                                          // Show labels
                },
                emphasis: {                                                                                           // Configure emphasis settings
                    focus: 'series'                                                                                     // Emphasize the entire series on interaction
                },
                data: [$01, 0, 0, 0, 0, 0, 0]
                
            }
        };
   
            if (option && typeof option === 'object') {                                                             // Check if the "option" object is valid
                 myChart.setOption(option);                                                                            // Apply the chart configuration options
                }

           window.addEventListener('resize', myChart.resize); 