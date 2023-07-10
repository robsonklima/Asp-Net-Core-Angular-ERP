import React, { useEffect, useState } from 'react';
import { View, FlatList, StyleSheet, Text } from 'react-native';
import api from '../services/api';

const styles = StyleSheet.create({
  container: {
    flex: 1,
    paddingTop: 22
  },
  item: {
    padding: 10,
    fontSize: 18,
    height: 44,
  },
})

export default function HomeScreen({ navigation }) {
  const [chamados, setChamados] = useState([]);

  useEffect(() => {
    (async () => {
      await api
        .get('OrdemServico', {
          params: {
            codTecnico: 1153,
            pageSize: 10
          },
          headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
          }
        })
        .then((response) => {
          const responseData = response.data.items;

          responseData.forEach(chamado => {
            setChamados(chamados.push(chamado));
          });
        })
        .catch(e => {
          console.log(e);
        });
    })();

  }, [])

  return (
    <div>

    </div>
  );
}
