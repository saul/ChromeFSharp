import React, { Component } from 'react';
import logo from './logo.svg';
import './App.css';

class App extends Component {
  constructor(props) {
    super(props);

    this.state = {
      counter: 0
    }

    window.myobject.register(counter => {
      this.setState({counter});
    });
  }

  increment() {
    window.myobject.increment();
  }

  render() {
    return (
      <div className="App">
        <div className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
          <h2>Welcome to React :)</h2>
        </div>
        <p className="App-intro">
          <button onClick={this.increment}>Increment</button><br />
          Current count: {this.state.counter}
        </p>
      </div>
    );
  }
}

export default App;
