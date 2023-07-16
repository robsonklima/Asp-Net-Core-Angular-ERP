import React from 'react';
import { ScrollView, StyleSheet } from 'react-native';
import { Block, Text, theme } from 'galio-framework';
import { nowTheme } from '../constants';
import { Card } from '../components';

class Chamado extends React.Component {
  renderChamado = () => {
    return (
      <Block style={styles.container}>
        <Text size={16} style={styles.title}>
          Chamado
        </Text>
      </Block>
    );
  };

  render() {
    return (
      <Block flex>
        <ScrollView showsVerticalScrollIndicator={false}>{this.renderChamado()}</ScrollView>
      </Block>
    );
  }
}

const styles = StyleSheet.create({
  container: {
    paddingHorizontal: theme.SIZES.BASE
  },
  title: {
    fontFamily: 'montserrat-bold',
    paddingBottom: theme.SIZES.BASE,
    marginTop: 44,
    color: nowTheme.COLORS.HEADER
  }
});

export default Chamado;
