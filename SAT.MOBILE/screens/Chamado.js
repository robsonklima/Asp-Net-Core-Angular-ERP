import React from 'react';
import {
  ScrollView,
  StyleSheet,
  Dimensions
} from 'react-native';

import { Block, Text, theme } from 'galio-framework';
import { nowTheme } from '../constants';

const { width } = Dimensions.get('screen');

class Chamado extends React.Component {
  constructor(props) {
    super(props);
    this.state = {};
  }

  renderInformacoes = () => {
    return (
      <Block flex style={styles.group}>
        <Block style={{ paddingHorizontal: theme.SIZES.BASE }}>
          <Text size={12} style={styles.title}>
            Informações
          </Text>
          <Text
            h1
            style={{
              fontFamily: 'montserrat-regular',
              marginBottom: theme.SIZES.BASE / 2
            }}
            color={nowTheme.COLORS.HEADER}
          >
            7879929
          </Text>
          <Text
            h3
            style={{
              fontFamily: 'montserrat-regular',
              marginBottom: theme.SIZES.BASE / 2
            }}
            color={nowTheme.COLORS.HEADER}
          >
            Banco do Brasil
          </Text>
          <Text
            p
            style={{
              fontFamily: 'montserrat-regular',
              marginBottom: theme.SIZES.BASE / 2
            }}
            color={nowTheme.COLORS.HEADER}
          >
            Agência Andradas, Série 8773361172
          </Text>
          <Text style={{ fontFamily: 'montserrat-regular' }} muted>
            Rua Andradas 1342, Centro, Porto Alegre
          </Text>
        </Block>
      </Block>
    );
  };

  renderRAT = () => {
    return (
      <Block flex style={styles.group}>
        <Block style={{ paddingHorizontal: theme.SIZES.BASE }}>
          <Text size={12} style={styles.title}>
            Relatório de Atendimento
          </Text>
          <Text style={{ fontFamily: 'montserrat-regular' }} muted>
            Nenhum registro.
          </Text>
        </Block>
      </Block>
    );
  };

  renderDetalhes = () => {
    return (
      <Block flex style={styles.group}>
        <Block style={{ paddingHorizontal: theme.SIZES.BASE }}>
          <Text size={12} style={styles.title}>
            Detalhes
          </Text>
          <Text style={{ fontFamily: 'montserrat-regular' }} muted>
            Nenhum registro.
          </Text>
        </Block>
      </Block>
    );
  };

  renderFotos = () => {
    return (
      <Block flex style={styles.group}>
        <Block style={{ paddingHorizontal: theme.SIZES.BASE }}>
          <Text size={12} style={styles.title}>
            Fotos
          </Text>
          <Text style={{ fontFamily: 'montserrat-regular' }} muted>
            Nenhum registro.
          </Text>
        </Block>
      </Block>
    );
  };

  renderLaudos = () => {
    return (
      <Block flex style={styles.group}>
        <Block style={{ paddingHorizontal: theme.SIZES.BASE }}>
          <Text size={12} style={styles.title}>
            Laudos
          </Text>
          <Text style={{ fontFamily: 'montserrat-regular' }} muted>
            Nenhum registro.
          </Text>
        </Block>
      </Block>
    );
  };

  render() {
    return (
      <Block flex center>
        <ScrollView
          showsVerticalScrollIndicator={false}
          contentContainerStyle={{ paddingBottom: 30, width }}
        >
          {this.renderInformacoes()}
          {this.renderRAT()}
          {this.renderDetalhes()}
          {this.renderFotos()}
          {this.renderLaudos()}
        </ScrollView>
      </Block>
    );
  }
}

const styles = StyleSheet.create({
  title: {
    fontFamily: 'montserrat-bold',
    paddingBottom: theme.SIZES.BASE,
    color: nowTheme.COLORS.HEADER
  },
  group: {
    paddingTop: theme.SIZES.BASE * 2
  },
});

export default Chamado;
