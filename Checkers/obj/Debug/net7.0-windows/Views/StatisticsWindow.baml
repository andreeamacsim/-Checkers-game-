   M S B A M L   `   `   `  ����C  ?Checkers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Checkers.ViewsCheckers.Views  5   	   %    Checkers.Views.StatisticsWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:Checkers.Views     5   	     � Title$  StatisticsWindow��5   	   $	��450��6"   $	��800��6/   .��5	      � $��20q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��White wins:��6   $��Center=�6'   $
��Bold"�6B   �� 5   
   $��1��6/   $��Center=�6?    �� TextAlignment$ Right=�6Z   ��6   �� *	WhiteWins+6Z   �� 5   
   $��	Red wins:��6   $��1��6%   $��Center=�62   $
��Bold"�6M   �� 5   
   $��1��6-   $��1��6:   $��Center=�6J   $ Right=�6e   ��6   �� *	RedWins+6e   �� 5   
   $��Max pieces remaining:��6   $��2��61   $��Center=�6>   $
��Bold"�6Y   �� 5   
   $��2��68   $��1��6E   $��Center=�6U   $ Right=�6p   ��6   �� *MaxPiecesRemaining+6p   5       5!      