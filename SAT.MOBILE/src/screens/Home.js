import React, { useState, useEffect } from 'react';
import { FlatList, StyleSheet, Text, View } from 'react-native';
import AsyncStorage from '@react-native-async-storage/async-storage';
const globals = require('../models/Globals');

const styles = StyleSheet.create({
  container: {
    flex: 1,
    paddingTop: 22,
  },
  item: {
    padding: 10,
    fontSize: 18,
    height: 44,
  },
});

export default function HomeScreen({ navigation }) {
  const [chamados, setChamados] = useState([]);

  const onScreenLoad = async () => {
    const usuarioJSON = await AsyncStorage.getItem('usuario');
    const usuario = JSON.parse(usuarioJSON);
    const token = await AsyncStorage.getItem('token');

    const url = `${globals.BASE_URL}/OrdemServico?codTecnico=${usuario.codTecnico}&pageSize=10`;

    fetch(url, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`
      }
    })
      .then((response) => console.log(response.json()))
      .catch((error) => console.error(error))
      .finally();
  }

  useEffect(() => {
    onScreenLoad();
  }, []);

  return (
    <View style={styles.container}>
      <FlatList
        data={chamados}
        renderItem={({ chamado }) => <Text style={styles.item}>{chamado?.codOS}</Text>}
      />
    </View>
  )
}
