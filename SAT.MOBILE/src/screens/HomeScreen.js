import React, { useEffect, useState } from 'react';
import { StyleSheet } from 'react-native';
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
  const [teste, setTeste] = useState({ value: '', error: '' })

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
          setTeste({ value: 'TESTE 2', error: '' })
          console.log(teste.value)
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
