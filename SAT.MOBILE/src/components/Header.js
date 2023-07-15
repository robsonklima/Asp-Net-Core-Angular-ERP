import React from 'react'
import { Appbar } from 'react-native-paper'

export default function Header(props) {
  return (
    <Appbar.Header>
      <Appbar.Content title={props.title} />
    </Appbar.Header>
  )
}