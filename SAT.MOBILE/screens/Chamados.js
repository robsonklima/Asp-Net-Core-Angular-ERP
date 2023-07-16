import React from 'react';
import { ScrollView, StyleSheet } from 'react-native';
import { Block, Text, theme } from 'galio-framework';

import { articles, nowTheme } from '../constants';
import { Card } from '../components';

class Chamados extends React.Component {
  renderChamados = () => {
    return (
      <Block style={styles.container}>
        <Text size={16} style={styles.title}>
          Meus Chamados
        </Text>
        <Card item={articles[0]} horizontal />
        <Card item={articles[1]} horizontal />
      </Block>
    );
  };

  render() {
    return (
      <Block flex>
        <ScrollView showsVerticalScrollIndicator={false}>{this.renderChamados()}</ScrollView>
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

export default Chamados;
