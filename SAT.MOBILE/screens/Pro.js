import React from "react";
import {
  StyleSheet,
  Dimensions
} from "react-native";
import { Block, Text, theme } from "galio-framework";

const { height, width } = Dimensions.get("screen");

import nowTheme from "../constants/Theme";

class Pro extends React.Component {
  render() {
    const { navigation } = this.props;

    return (
      <Block flex style={styles.container}>
        <Block flex space="between" style={styles.padded}>
          <Block middle flex space="around" style={{ zIndex: 2 }}>
            <Block center style={styles.title}>
              <Block>
                <Text color="white" size={60} style={styles.font}>
                  SAT Mobile
                </Text>
              </Block>
            </Block>
          </Block>
        </Block>
      </Block>
    );
  }
}

const styles = StyleSheet.create({
  container: {
    backgroundColor: theme.COLORS.BLACK
  },
  padded: {
    top: 270,
    paddingHorizontal: theme.SIZES.BASE * 2,
    position: 'absolute',
    bottom: theme.SIZES.BASE,
    zIndex: 2
  },
  button: {
    width: width - theme.SIZES.BASE * 4,
    height: theme.SIZES.BASE * 3,
    shadowRadius: 0,
    shadowOpacity: 0
  },
  title: {
    marginTop: "-5%"
  },
  subTitle: {
    marginTop: 20
  },
  pro: {
    backgroundColor: nowTheme.COLORS.BLACK,
    paddingHorizontal: 8,
    marginLeft: 3,
    borderRadius: 4,
    height: 22,
    marginTop: 0
  },
  font: {
    fontFamily: 'montserrat-bold'
  }
});

export default Pro;
