import React from 'react'
import { StyleSheet } from 'react-native'
import { Appbar } from 'react-native-paper'
import { theme } from '../core/theme'

export default function Header(props) {
  return <Appbar.Header>
    <Appbar.Content title="SAT Mobile" />
    <Appbar.Action icon="cog" onPress={() => { }} />
    <Appbar.Action icon="account" onPress={() => { }} />
  </Appbar.Header>

}

const styles = StyleSheet.create({
  header: {
    fontSize: 21,
    color: theme.colors.primary,
    fontWeight: 'bold',
    paddingVertical: 12,
  },
})
