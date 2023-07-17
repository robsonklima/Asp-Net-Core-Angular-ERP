import React from "react";
import { Text, StyleSheet, Dimensions } from "react-native";
import { Block } from "galio-framework";

const { width } = Dimensions.get("screen");

class Home extends React.Component {
  renderPage = () => {
    return (
      <Text style={styles.title}>
        Home Page
      </Text>
    );
  };

  render() {
    return (
      <Block flex center style={styles.home}>
        {this.renderPage()}
      </Block>
    );
  }
}

const styles = StyleSheet.create({
  home: {
    width: width
  },
  title: {
    fontSize: 14,
    fontWeight: "bold",
    marginTop: 60
  }
});

export default Home;
