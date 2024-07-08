import 'dart:async';
import 'dart:io';
import 'package:flutter/material.dart';
import 'package:charts_flutter/flutter.dart' as charts;

void main() {
  runApp(VolumeCalculatorApp());
}

class VolumeCalculatorApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Volume Calculator',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: VolumeDashboard(),
    );
  }
}

class VolumeDashboard extends StatefulWidget {
  @override
  _VolumeDashboardState createState() => _VolumeDashboardState();
}

class _VolumeDashboardState extends State<VolumeDashboard> {
  List<VolumeData> volumeData = [];

  @override
  void initState() {
    super.initState();
    _fetchVolumeData();
    Timer.periodic(Duration(minutes: 1), (timer) => _fetchVolumeData());
  }

  void _fetchVolumeData() async {
    List<VolumeData> data = await getVolumes();
    print("Fetched Volume Data: ${data.length} volumes");
    setState(() {
      volumeData = data;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Volume Calculator'),
      ),
      body: Padding(
        padding: const EdgeInsets.all(8.0),
        child: GridView.count(
          crossAxisCount: 3,
          children: volumeData.map((volume) => VolumeChart(volume)).toList(),
        ),
      ),
    );
  }
}

class VolumeData {
  final String name;
  final double usedSpace;
  final double totalSpace;

  VolumeData(this.name, this.usedSpace, this.totalSpace);
}

class VolumeChart extends StatelessWidget {
  final VolumeData volume;

  VolumeChart(this.volume);

  @override
  Widget build(BuildContext context) {
    List<charts.Series<ChartData, String>> series = [
      charts.Series(
        id: volume.name,
        domainFn: (ChartData data, _) => data.label,
        measureFn: (ChartData data, _) => data.value,
        data: [
          ChartData('Used', volume.usedSpace),
          ChartData('Free', volume.totalSpace - volume.usedSpace),
        ],
        colorFn: (ChartData data, _) => charts.ColorUtil.fromDartColor(
            data.label == 'Used' ? Colors.blue : Colors.blue[100]!),
        labelAccessorFn: (ChartData data, _) =>
            '${data.value.toStringAsFixed(1)} GB',
      ),
    ];

    return Card(
      child: Padding(
        padding: const EdgeInsets.all(8.0),
        child: Column(
          children: [
            Text(
                '${volume.usedSpace.toStringAsFixed(1)}GB / ${volume.totalSpace.toStringAsFixed(1)}GB'),
            Expanded(
              child: charts.PieChart<String>(
                series,
                animate: true,
                defaultRenderer: charts.ArcRendererConfig(
                  arcWidth: 60,
                  arcRendererDecorators: [charts.ArcLabelDecorator()],
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}

Future<List<VolumeData>> getVolumes() async {
  List<VolumeData> volumes = [];

  try {
    var result = await Process.run(
        'wmic', ['logicaldisk', 'get', 'size,freespace,caption']);
    if (result.exitCode == 0) {
      var lines = result.stdout.split('\n');
      for (var line in lines) {
        var parts = line.trim().split(RegExp(r'\s+'));
        if (parts.length == 3 && parts[0] != 'Caption') {
          var name = parts[0];
          var freeSpace = double.tryParse(parts[1]) ?? 0;
          var totalSpace = double.tryParse(parts[2]) ?? 0;
          var usedSpace = (totalSpace - freeSpace) / (1024 * 1024 * 1024);
          totalSpace = totalSpace / (1024 * 1024 * 1024); // Convert to GB
          if (totalSpace > 0) {
            volumes.add(VolumeData(name, usedSpace, totalSpace));
          }
        }
      }
    }
  } catch (e) {
    print('Error accessing drive: $e');
  }

  print("Discovered Volumes: ${volumes.length}");
  return volumes;
}

class ChartData {
  final String label;
  final double value;

  ChartData(this.label, this.value);
}
