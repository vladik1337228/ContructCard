   M S B A M L   `   `   `  ���� G  CContructCard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:ContructCardContructCard  c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity     ContructCard.MainWindow   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:ContructCard     ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     Be:http://schemas.microsoft.com/expression/2010/interactivity    � Title$  
MainWindow��$	��450��$	��800�� � SizeToContent$ WidthAndHeight=�$	��470��.  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger     	EventName$ Closed��.  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� *Close+   CommandParameter ��  �� ElementName$ 
imageBrush�� � 	Resources    ContructCard.MyConvert   myConverter#   ContructCard.MyConverterSharp  convertSharp  Pattern	 �� Source��%D   &	        &	      &	 
         �� $	 Dictionary2.xaml?�.��� 
 � RowDefinitions
 �� $	��53* �$��53��$��53���� $	��40* �$��40��$��40���� $	��40* �$��40��$��40���� $	��40* �$��40��$��40���� $
��165* �$	��165��$	��165���� �� $��37 ��� $��38 �$��38��$��38���� $
��43.5 � � ColumnDefinitions �� $
��263* �$	��263��$	��263���� $
��128* �$	��128��$	��128���� $
��100* �$	��100��$	��100���� $
��178* �$	��100���� $	��72* �$	��100���� $	��59* �$	��100��.+��� -    ��Name$ 	comboBox1��$	��Top=�$��1��$��0�����	#FFA13535$��30��$��30��$��10,10,10,10q����	#FFD93434
���   �$��11(�$
��Bold"�$��Normal$�$��2�� �� SelectedIndex �� *PatternCard+���� *Cards.CollectionCard+   $ SelectionChanged��.    �� *PatternCardChangePatter+���� .R��� $	��Top=����� *
NameCard+ � ToolTip �� *ToolTipMessage+�� -    ��Name$ button1��$	��Top=�$��0��$��8��$
��Save��$��	10,8,10,0q�$��33��$	��243��$
��Bold"�$��14(�$��23������ *Save+���� $ panel���� -    ��Name$ panel��$	��338��$��0��$��0��$��5�� J� Opacity$ 1Q�$
��Auto��$
��Auto��.+��� -    ��Name$ image�� K� ZIndex$
 1000��$��0��$��Uniform=�$��4��$��1��$	��331��$	��258������ $ 	comboBox1�� �� Path$ SelectedItem.PathImage��� -    ��Name$ imageEllips��  RectangleSettings#���  $��0��$��1��$��4��$
 1001��$	��194�� �� Left$ 32������ -   
imageBrush�� j� Stretch$ UniformToFill=� j� TileMode$
 None=� j� Viewbox$ 0,0,1,1� j� Viewport$ 0,0,1,1� j� ViewportUnits$ RelativeToBoundingBox=� j� ViewboxUnits$ RelativeToBoundingBox=� j� 
AlignmentY �� *
AlignmentY+ j� 
AlignmentX �� *ImageX+ �� ImageSource �� *	ImagePath+  �� RelativeTransform  V� .~��� ! �� CenterY$	! 0.5Q�" �� CenterX$	" 0.5Q�# �� ScaleX# �� $ slider4��$ Value�$ �� ScaleY$ �� $ slider4��$ Value�T� �� -   $ 
imageSkill��  ImageVisible#���  $
 1001��$	 100��
% �� Top$	% 261������ $ comboBoxSkill��$ SelectedItem.PathImage��� -   $ imageSkill2��  ImageVisible2#���  $
 1001������ $ comboBoxSkill2��$ SelectedItem.PathImage��� $) #Dmg после взросления��  TextBoxVisible#���  $ ��./#Fixedsys Excelsior 3.01)�$
 1001��$��16��$��16��$ 98��$	% 302��$��13(����� *DmgDop+��Z� ��Z� �� $A ;Длительность взросления в ходах��#���  $ ��./#Fixedsys Excelsior 3.01)�$
 1001��$��16��$��16��$	 142��$	% 259��$��13(����� *CountStepDop+��Z� ��Z� �� $( "HP после взросления��#���  $ ��./#Fixedsys Excelsior 3.01)�$
 1001��$��16��$��16��$	 142��$	% 302��$��13(����� *HPDop+��Z� ��Z� ,�   TextBoxSettings#���  $ ��./#Fixedsys Excelsior 3.01)�$|�False��"& ,� VerticalScrollBarVisibility$& Hidden=�$' ,� HorizontalScrollBarVisibility$' Hidden=�$
 1001��$��4��$��0��$ 17��$	��226������ *FlowDocument+��   	LabelText#���  $��./#Perfocard 4F Bold)�
���  ��$��Center=�$��Stretch=�$
 1001��$��1��$��2��$��39��$% 16��$	 181��$��24������ $ slider��$ Value�( �� 	Converter#( [  ) �� ConverterParameter$) 3����  	 LabelVisible#��� 	 $��./#Perfocard 4F Bold)�
���  ��$��Center=�$��Stretch=�$
 1001��$��4��$��25(�$	 175��$	% 282��$��46������ $ slider1��$ Value�#( [  $) 2���� #��� 	 $��./#Perfocard 4F Bold)�
���  ��$��Center=�$��Stretch=�$
 1001��$��4��$��25(�$��Normal$�$	% 282��$ 37��$��46��$��47������ $ slider2��$ Value�#( [  $) 2����  
 TitleCardSetting#��� 
 $ ��./#Fixedsys Excelsior 3.01)�
���   �* �� TextAlignment$* Center=�$
 1001��$% 19��$��19������ $ FontSizeBoxTitle��$ SelectedItem.FontSize����� $ 	titleCard��$
 Text��� -	   + ��Name$+ nuber��  TargetTextBloc#���  $ ��./#Fixedsys Excelsior 3.01)�$
 1001��
���   �$* Center=�$ 31��$% 20��$��15��$��33������ *
NumberCard+#( [ ��   TargetTextBlocVisible#���  $ ��./#Fixedsys Excelsior 3.01)�$
 1001��
�������$* Center=�$ 26��$% 3��$��13��$
��Auto������ $ typeCard��$ SelectedItem.Content��� -
   , ��Name$, slider��$��Center=�$��1��$��1��- � Maximum$- 10Q�$	��100��$	��100��$��	8,10,16,8q�. � SmallChange$. 1Q�/ �� TickPlacement$/ BottomRight=�0 �� AutoToolTipPlacement$
0 None=�1 �� Interval$1 1��2 �� IsSnapToTickEnabled$
2 True��3 � Value3 �� *Mana+�� -   $, slider1��$��Center=�$��1��$��2��$- 10Q�$	��100��$	��100��$��	8,10,16,8q�$
2 True��$1 1��$. 1Q�$/ BottomRight=�3 �� *Hp+�� -   $, slider2��$��Center=�$��1��$��3��$- 10Q�$	��100��$	��100��$��	8,10,16,8q�$. 1Q�$/ BottomRight=�$1 1��$
2 True��3 �� *Dmg+�� -   $, slider3��$��Center=�$��0��$��6��$- 2Q�$	��100��$	��100��$��120,10,16,8q�$/ BottomRight=�$1 1��$
2 True��$. 1Q�3 �� *ValueSlider3+   $ ValueChanged��.    �� *SliderValue+�� -   $, sliderY��$��0��$��6��$- 2Q�$	��100��$	��100��$��-63,10,87,8q�$/ BottomRight=�$1 1��$
2 True��$. 1Q�3 �� *ValueSliderY+   $ ValueChanged��.    �� *SliderValueY+�� -   $, slider4��$��Center=�$��0��$��7��$- 2Q�$	��100��$	��100��$��	8,10,16,8q�$/ BottomRight=�$1 1��4 � Minimum$4 1Q�3 �� *ImageY+�� $��	Text card��$��1��$��4��$��
2,0,64,140q��� -   $ ButonAddImageInText��$-��'Добавить фото в текст��$��1��$��4��$��69,2,15,138q�$��2������ *AddImageInText+���� $ textbox��$
 Text��� -   5 ��Name$5 textbox��6 �� TextWrapping$
6 Wrap=�7 �� AcceptsReturn$
7 True��$��4��$��1��$��2��$��	5,30,15,0q����� *
TextCard+.��   $ TextChanged��.    �� *TextChanged+ �� $ textbox��$
 Text�.���� -   $ FontSizeBox��$��1��$��6��$��5,5,5,5q� �� *TextFontSize+���� *CollectionSize+���� .R��� 8 �� ContentStringFormat$8 {0}px������ *
FontSize+   $ SelectionChanged��.    �� *SizeChanged+�� $��	Font size��$��2��$��6��$��Center=�$��Center=��� $��1��$��2��$
��Mana��$��Center=�$��Center=�$��26��$��40���� $��3��$��2��$	��DMG��$��Center=�$��Center=�$��26��$��38���� $��2��$��2��$��HP��$��Center=�$��Center=�$��26��$��26���� -   $5 	titleCard��$��1��$��7��$��5,5,5,5q����� *	TitleCard+�� $��
Title card��$��2��$��7��$��Center=�$��Center=��� -   $ FontSizeBoxTitle��$��1��$��8��$��5,5,5,6q� �� *TitleFontSize+���� *CollectionSizeTitle+���� .R��� $8 {0}px������ *
FontSize+�� $��Font size title��$��8��$��2��$��Center=�$��Center=��� $��3��$��0��$��12,12,12,12q�$��
Load photo��$
��Bold"�$��15(�$��3������ *
OpenDialog+�� -   $ typeCard��$
��Bold"�$��14(�$��1��$��3��$��12,5,5,5q� �� *
TypeCard+.@��� $��Standart��$��Center=�$
��Bold"��� $
��Rare��$��Center=�$
��Bold"��� $��
Super rare��$��Center=�$
��Bold"��� $��	Legendary��$��Center=�$
��Bold"��� $��1��$��4��$��Rarity��$��Center=�$��Center=��� -   $ comboBoxSkill��$��Center=�$��3��$��2��$��30��$��30��$��12,5,5,5q����	#FFD93434
���   �$��11(�$
��Bold"�$��Normal$� �� *	SkillCard+���� *Skills.CollectionSkill+   $ SelectionChanged��.    �� *PatternSkillChangePatter+ �� $ comboBoxSkill��$ SelectedItem����� .R��� $	��Top=����� *	PathImage+ �� *ToolTipMessage+�� $��Skill��$��2��$��4��$��Center=�$��Center=��� -   $ comboBoxSkill2��  ComboBoxVisible#���  $��Center=�$��3��$��3��$��30��$��30��$��12,5,5,5q����	#FFD93434
���   �$��11(�$
��Bold"�$��Normal$� �� *
SkillCard2+���� *Skills.CollectionSkill+   $ SelectionChanged��.    �� *PatternSkillChangePatter+ �� $ comboBoxSkill2��$ SelectedItem����� .R��� $	��Top=����� *	PathImage+ �� *ToolTipMessage+��   UnLabelVisible#���  $��Second Skill��$��3��$��4��$��Center=�$��Center=��� $��3��$��7��$��	12,5,12,5q�$��Save pattern��$
��Bold"�$��14(�$��3������ *SaveElements+�� $��3��$��8��$��	12,5,12,6q�$��Load pattern��$
��Bold"�$��14(�$��3������ *LoadElements+�� -   9 ��Name$9 checkBox��$��2��$��5��$��Second skill��$��Center=�$��16��: d� 	IsChecked: �� *SecondSkill+   $ 	Unchecked��.    �� *SecondSkillCheck+  $ Checked��.    �� *SecondSkillCheck+�� -   $ openFlowDocument��$0��*Желательно к прочтению��$��3��$��6��$��3��$��	12,5,12,5q�$
��Bold"�$��14(�� $��3��$��4��$��5,5,5,5q�$��3��.+�� 
 �� �� $��20 ���  �� �� $��* �.+��� $��0������ $ comboBoxSkill��$ SelectedItem.PathImage��� $* Justify=�$��10,0,0,0q�$
��Wrap=�$��1��$��0������ *SelectedItem.ToolTipMessage+$ comboBoxSkill���� $��0��$��2������ $ comboBoxSkill2��$ SelectedItem.PathImage��� $* Justify=�$��10,0,0,0q�$
��Wrap=�$��1��$��2������ *SelectedItem.ToolTipMessage+$ comboBoxSkill2��