import React from 'react'
import { Appbar } from 'react-native-paper'

export default function Header(props) {
  return (
    <Appbar.Header>
      <Appbar.Content title="SAT Mobile" />
      <Appbar.Action icon="dots-vertical" onPress={() => { }} />
    </Appbar.Header>
  )
}