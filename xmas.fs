\ christmas tree     uh 2016-12-24

0 Variable line#
0 Variable col#

: home
    0 line# ! 0 col# ! ;

: show ( -- ) flush ;

: cls ( -- )
    buffer-off home ;

: line ( rgb0 rgb1 rgb2 rgb3 rgb4 rgb5 rgb6 rgb7 -- )
    8 0 DO  line# @ I  xy!  LOOP ;

: nl ( -- ) 1 line# +! 0 col# ! ;

: ~ ( -- )  1 col# +! col# @ 7 > IF nl THEN ;

$675672 Variable seed
$10450405 Constant generator

: rnd ( -- ) seed @ generator um* drop 1 + dup seed ! ;

: random ( n -- 0..n-1 ) rnd um* nip ;

: ? ( -- )
    $10 random $0B < IF ~ EXIT THEN
    \    $10 random $30 +  $010100 * line# @ col# @ xy! ~ ;
    line# @ col# @ xy@ $00FF00 and 8 rshift 
    $10 random 8 - + $50 min  $10 max
    $010100 * line# @ col# @ xy! ~ ;


$000000 Constant #black
$001F00 Constant #green
$0F0800 Constant #brown
$1F0000 Constant #red
$3F3F00 Constant #yellow

: Pixel ( color -- ) <builds ,
  does> @ line# @ col# @ xy!  ~ ;

#black  Pixel _
#green  Pixel #
#brown  Pixel %
#yellow Pixel ^
#red    Pixel &

: triangle ( -- )
    _ _ _ # # _ _ _
    _ _ # # # # _ _
    _ # # # # # # _
    # # # # # # # # ;

: trunk ( -- )
    _ _ _ % % _ _ _ 
    _ _ _ % % _ _ _ ;


: candles1 ( -- )
    ~ ~ ~ ~ ~ ~ ~ ~ 
    ~ ~ ~ ^ ~ ^ ~ ~ 
    ~ ^ ~ & ~ & ~ ~ 
    ~ & ~ & ~ ~ ~ ~ ;

: candles2 ( -- )
    ~ ~ ~ ~ ~ ~ ~ ~ 
    ~ ~ ^ ~ ^ ~ ~ ~ 
    ~ ~ & ~ & ~ ^ ~
    ~ ~ ~ ~ & ~ & ~ ;


: flicker1 ( -- )
    ~ ~ ~ ~ ~ ~ ~ ~ 
    ~ ~ ~ ? ~ ? ~ ~ 
    ~ ? ~ ~ ~ ~ ~ ~ 
    ~ ~ ~ ~ ~ ~ ~ ~ ;

: flicker2 ( -- )
    ~ ~ ~ ~ ~ ~ ~ ~ 
    ~ ~ ? ~ ? ~ ~ ~ 
    ~ ~ ~ ~ ~ ~ ? ~
    ~ ~ ~ ~ ~ ~ ~ ~ ;

: tree ( -- )
    cls
    7 0 DO triangle LOOP
    trunk
    home 3 0 DO candles1 candles2 LOOP candles1
    show
    BEGIN 500 random 100 + ms  key? 0= WHILE
      home 3 0 DO flicker1 flicker2 LOOP flicker1
      show
    REPEAT ;
 
