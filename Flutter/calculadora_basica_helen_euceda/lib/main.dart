import 'package:flutter/material.dart';

void main() => runApp(const ScaffoldExampleApp());

class ScaffoldExampleApp extends StatelessWidget {
  const ScaffoldExampleApp({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: const ScaffoldExample(),
      theme: ThemeData(
        scaffoldBackgroundColor: const Color.fromARGB(255, 0, 0, 0),
      ),
      debugShowCheckedModeBanner: false,
    );
  }
}

class ScaffoldExample extends StatefulWidget {
  const ScaffoldExample({Key? key}) : super(key: key);

  @override
  State<ScaffoldExample> createState() => _ScaffoldExampleState();
}

class _ScaffoldExampleState extends State<ScaffoldExample> {
  int _count = 10;

  void _incrementCounter() => setState(() => _count++);
  void _decrementCounter() => setState(() => _count--);
  void _multiplyCounter() => setState(() => _count *= 2);
  void _divideCounter() => setState(() => _count ~/= 2);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Center(
          child: Text(
            'Calculadora Básica 📒',
            style: TextStyle(
              fontSize: 30,
              fontWeight: FontWeight.w900,
              color: Color.fromARGB(255, 255, 255, 255),
            ),
          ),
        ),
        backgroundColor: const Color.fromARGB(255, 2, 43, 155),
      ),
      body: Center(
        child: Text(
          ' $_count',
          style: const TextStyle(
            fontSize: 30,
            fontWeight: FontWeight.bold,
            color: Color.fromARGB(255, 255, 255, 255),
          ),
        ),
      ),
      bottomNavigationBar: BottomAppBar(
        shape: const CircularNotchedRectangle(),
        color: const Color.fromARGB(255, 0, 0, 0),
        child: Row(
          mainAxisAlignment: MainAxisAlignment.spaceEvenly,
          children: [
            IconButton(
              icon: const Icon(Icons.add_circle_outline),
              onPressed: _incrementCounter,
              color: Colors.white,
            ),
            IconButton(
              icon: const Icon(Icons.remove_circle_outline),
              onPressed: _decrementCounter,
              color: Colors.white,
            ),
            IconButton(
              icon: const Icon(Icons.cancel_outlined),
              onPressed: _multiplyCounter,
              color: Colors.white,
            ),
            IconButton(
              icon: const Icon(Icons.percent_sharp),
              onPressed: _divideCounter,
              color: Colors.white,
            ),
          ],
        ),
      ),
    );
  }
}
