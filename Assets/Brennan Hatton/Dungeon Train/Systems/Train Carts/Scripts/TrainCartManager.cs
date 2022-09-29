using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using BrennanHatton.Math;

namespace BrennanHatton.TrainCarts
{
	
	public class TrainCartManager : MonoBehaviour
	{
		public TrainCartGenerator generator;
		public GameObject endCarriage;
		
		public int totalTrainCarts = 3;
		public List<TrainCart> carts = new List<TrainCart>();
		
		float position = 0;
		
		//int roomId = 0;
		
		public BigInteger roomId;
		public int seedBase = 35;
		public int cartsSharingSeed = 2;
		
	    // Start is called before the first frame update
	    void Start()
		{
			string testStr = "0v2x1w4xz2qvbwjw2eawfp0ei21lkld9r53lq7szcxb56ql1oypwx0fxmgt2y0nnbfgppa5816emfjg7tsrh2yizcj6h0qzffx8jm6nxjj7u9gulku3csso72t6lu22nbrh1jnpoj5fureh1stp4co1k6w42an6wm07hkn2jp7xqs4vefhel8n6h8c3m9ikpvv3y85i5oq43te4ucf7faloqjiihmrfhfelurzmobygbx6c4vbbumc6qxpg2vmhiuzradb5za5uk9f8as31mb8pzm016ngdma6hsub948qioprwuzk2i9d6ur2dwtvyjwl3dxaensnsu6aq926b7i9z1zh2sielj1eb4it4bq2fobapxnphfnhhro28m6zr2j34phfgg8d16lgc0aogzht0yks6b9s9s95rvejjfekd8eicpyx8vp4dtty0cgn11r0ffdd9s4pfsxz251pv4fmef3z0mdmqpns5znkxg4oti68gn2tb9hcq66whxb5ptity1e4zu0ozumb948yz81azlkfxjch8omgh01t3i54xv163pejn8beo096kei5xonn4vrjpno7gy7zsgi5ofjtwpyut3djtu93rwmijvj271lsekrn6b2ccy8ymzqyi5h7edcma924tz2z923m7ao1bfp5leu9f26o49e7dn24v0zwpczfuxip84q0p2xxjtomgx7i8cfzmru8iebeanbaf4sosj08o7xvruo21kuh1qp8467hmu10u4lx6ag8k0c9mp2i7vipaag2260x5mlvqy4zjxqhi1t71f9yvs1hn7qtp7ycg35jvk5ky6sxb3rgrah6zg2fvik6thyupgu009d0ld5j4k673obewyamr9nutpgqnz93648wtrhei8pipv574vjs2j146n8pidi7eh4n1k1l8fhz0zixoeek4v71pu826adj4lm1hu6hgqwxd8mv293f4kipuv749k8m0ii1rw4cixb8uqmirg2vjmobwt8lahwat0vj1223onp4y2evyraaztay07ve9cz634saaybjqtto3rl1dcvrfo22hosmsjxeb45fd4rh8vjlvjdu9128x61yzvoyt1xtb4ex491ezuvzxtblss6aap3omg59hlji95tim3zyp6d7tt0ws1j3p0q2twpok5r5y99uod1vw4hia5ku9xve1atv37eq6deah1hxazdylskgudfixbnej0vrb9f51ts4mo3nm90n2hc9bpzg0sixzhnw9q9pttyihgduwiq4t1rg9lt8exx5cqonvjn2bnh9yvh470ny54vh5pbaum4k1nsvh3elpyq5y7jjs4aj8yhhdhgxiquim3fwdx4vexjprq5idhvnuhy539k8kpa8fjoibx470hvcxr3qfxmotrwxfxqtghm8ldvm9u09qnqi2y6awxqk7ircmr44zrcrnktaujzlqzfgdqsv3psl8u49bflhve05tylm98uuqgae4oc3neeste286gahq8gdgd1sz3b41d8cnbwqk054b5ye37ihw3u8i23zrvo0l7skxpgtodsi8c8f42fglxigev9h3hdsuf1haxr4fuj0nd0qt2wqxmqvjh09b6uk777lbav0m75xyt29we5f3mnory539f4xs8baeprkac9y5swmgq84qcqdnivgkc2criytz2yaz1qi3ypzi991gjz5f1gbr623pbtvtm5uni1et99mq791tpp60f1to3so3sm9djblkccwvvft1ei992cdde75kbr1y8iqg2n24rftnbf7ovh4t6ebr73tj57ero9cxz2urgzeor232tqf63vff1rylwjdjfm5bnaudikh4rb8kxbng3xj8xj1y4jdrw0bi8zw5q67239q66rzbkgkmbl78ap2asulu6qb0cba523g6pox26qf7kkzol4jkz96o3mbfltruufhoi5xcese2k2t8gov5ctpflzhpk1zrhl1eg4xmzvduj8v0u5tsllncejh6vprk9t0zbe7umbwsw06n8atcy366lihxo5mbgboc2uhcwr92vyvb12xxu9y7m8odtv1v26gxnl55yalqjbregwa3yirjm7l6zkgx0ers43entjh26ffyvrywuottsbwr0n12vpnbxs3ulxai9w0bzyryeeepm8xf5wt3ha1p1r0ylrkey900zdvn7gvknebsmswleaw2pki6uh5bb4lnq7om6ujk7jlq4mj1rs232etzwiny3q56djb4ne5dk98sr9kfdfvmq29jivkfdiwqo89jy9wkhiw8zakrj0iluo3yxlxejeivhgj0fl4f9qh6ry5kn34e4kafa2r11zb167rmmyl1art24k3d530cvt7jz6gv62dl6lbc3d4qtfm5zdvma3zywh98xscvioe1ttmyiegboki9ggkpdvr66rtc50y02m7qe45eju9h8cmhem7xayv4o9yu1z9ydv0y9krpk3qh4dkmwngu5428s9e48djkcwvfc1gghrhi6ufsdfudniczyxodnjbj7a2t5t8fgicgf2yn8fwhif0v6l5gqh092riyzto91issndok42qmkx5b2ebl9q48tjviwuxyxwqohr11tul5b1gqeerrkmixw82ug3yddf8qliy3iyq0791yborwdta032vdtyvu4x07bk787zkcade0u1qyelp4zclblk4jf33m94x7ricbvlt4kk9fzqatrg4bshiez8ombk4oaanva3q3n0z2bj0e4bqbja8uiafjc29vu8mlx6os9sg0gm7ponh3mygwro9e46bonybavxx1yw53sefn4ditvm6ywc0igz5tnxw10s7n7ijf1d4omvrv255cos3uuszg3p74qfrpix0fxntyebgko2wzblo5rmc2chrnnvyoe639t3e12sck1ko71q8mku77stnsmq59do7xdayrnez77i2i0onf3g6jf1ovqapz4b7mpn82plq4c8qg658wwon3l2qkxk7xik8tjx5ct6lfov87dniv7fky1y18qbu5rmkfecyd039ft1fv0uc15u92bx07n532bhr59esh1m1bzlwomp5u2h78x8tpna60fjc31mouokq712og070y5ypw0ar8c3j5pdwq10d4ueo06ofkaf85ce7rr4m4ao5trusmmdt117tue6k2wnd6g7pcbkdvgtzn04cpahcd4itsai2xkid1rnms7vdu78q446lsjemtdc3h1hcooz7", testStr2;
        
			Debug.Log(testStr);
			BigInteger intTest = MyBigIntegerExtensions.Parse(testStr, 36);
			Debug.Log(intTest);
			testStr2 = MyBigIntegerExtensions.ToRadixString(intTest, 36);
			Debug.Log(testStr2);
			Debug.Log(string.Compare(testStr, testStr2));
			
			
		    for(int i =0 ;i < totalTrainCarts; i++)
		    {
		    	AddCartToEnd();
		    }
	    }
	
	    // Update is called once per frame
	    void Update()
	    {
		    if(carts[1].playerInside)
		    {
		    	DestroyObject(carts[0].gameObject);
		    	carts.RemoveAt(0);
		    	
		    	AddCartToEnd();
		    }
	    }
	    
		int iCarts = 0;
		void AddCartToEnd()
		{
			bool first = (carts.Count == 0);
				
			TrainCart newCart;
			
			newCart = generator.CreateCart("0"+MyBigIntegerExtensions.ToRadixString(roomId,seedBase));
			iCarts++;
			if(iCarts == cartsSharingSeed)
			{
				roomId++;
				iCarts = 0;
			}
			//if(first)
			//	newCart - make itclose door
		    	
			newCart.transform.position += transform.forward*position;
		    	
			position += newCart.length;
			
			endCarriage.transform.position = transform.position + transform.forward*position;
			carts.Add(newCart);
		}
		
		public void TeleportToCart(string _seed)
		{
			Debug.Log(_seed);
			roomId = MyBigIntegerExtensions.Parse(_seed, seedBase);
			Debug.Log(roomId);
			string testStr2 = MyBigIntegerExtensions.ToRadixString(roomId, 36);
			Debug.Log(testStr2);
			Debug.Log(string.Compare(_seed, testStr2));
			
			position = carts[0].transform.position.z;
			
			for(int i =0 ;i < totalTrainCarts; i++)
			{
				DestroyObject(carts[i].gameObject);
			}
			
			for(int i =0 ;i < totalTrainCarts; i++)
			{
				AddCartToEnd();
			}
			
		}
	}
}