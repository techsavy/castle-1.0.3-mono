// Copyright 2004-2007 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Castle.MonoRail.Framework.Views.NVelocity.Tests
{
	using System;
	using Castle.MonoRail.Framework.Tests;
	using NUnit.Framework;

	[TestFixture]
	public class ComponentsTestCase : AbstractTestCase
	{
		[Test]
		public void CaptureForDirective()
		{
			DoGet("usingcomponent/capturefordirective.rails");

			AssertSuccess();

			AssertReplyContains(@"=navbar= some content =navbar=");
		}

		[Test]
		public void CaptureForComponent()
		{
			DoGet("usingcomponent/capturefor.rails");

			AssertSuccess();

			AssertReplyContains(@"=navbar= some content =navbar=");
		}

		[Test]
		public void CaptureForComponentAppend()
		{
			DoGet("usingcomponent/captureforappend.rails");

			AssertSuccess();

			AssertReplyContains(@"=2= some content =3=  =4=");
		}

		[Test]
		public void CaptureForComponentAppendBefore()
		{
			DoGet("usingcomponent/captureforappendbefore.rails");

			AssertSuccess();

			AssertReplyContains(@"=2= some content	=4= 	=3=");
		}

		[Test]
		public void InlineComponentUsingRenderText()
		{
			DoGet("usingcomponent/index1.rails");

			AssertSuccess();

			AssertOutput("static 1\r\nHello from SimpleInlineViewComponent static 2", Output);
		}

		[Test]
		public void InlineComponentUsingRender()
		{
			DoGet("usingcomponent/index6.rails");

			AssertSuccess();

			AssertOutput("static 1\r\nThis is a view used by a component static 2", Output);
		}

		[Test]
		public void InlineComponentUsingTemplatedRender()
		{
			DoGet("usingcomponent/inlinecomponentusingtemplatedrender.rails");

			AssertSuccess();

			AssertOutput("v1 v2 [contains items from property bag] v1 v2 [contains items from property bag]", Output);
		}

		[Test]
		public void InlineComponentNotOverridingRender()
		{
			DoGet("usingcomponent/index3.rails");

			AssertSuccess();

			AssertOutput("static 1\r\ndefault component view picked up automatically static 2", Output);
		}

		[Test]
		public void InlineComponentWithParam1()
		{
			DoGet("usingcomponent/index4.rails");

			AssertSuccess();

			AssertOutput("Done 1", Output);
		}

		[Test]
		public void BlockComp1()
		{
			DoGet("usingcomponent/index5.rails");

			AssertSuccess();

			AssertOutput("  item 0\r\n  item 1\r\n  item 2\r\n", Output);
		}

		[Test]
		public void BlockWithinForEach()
		{
			DoGet("usingcomponent/index8.rails");

			AssertSuccess();

			AssertOutput("inner content 1\r\ninner content 2\r\n", Output);
		}

		[Test]
		public void SeveralComponentsInvocation()
		{
			for(int i = 0; i < 10; i++)
			{
				DoGet("usingcomponent/index9.rails");

				AssertSuccess();

				AssertOutput("static 1\r\nContent 1\r\nstatic 2\r\nContent 2\r\nstatic 3\r\nContent 3\r\nstatic 4\r\nContent 4\r\nstatic 5\r\nContent 5\r\n", Output);
			}
		}

		[Test]
		public void ArrayListAsComponentParam()
		{
			DoGet("usingcomponent/index10.rails");

			AssertSuccess();

			AssertOutput("static 1\n1 2  static 2", Output);
		}

		[Test]
		public void ComponentWithInvalidSection()
		{
			DoGet("usingcomponent2/ComponentWithInvalidSections.rails");

			AssertReplyContains("The section 'invalidsection' is not supported by the ViewComponent 'GridComponent'");
		}

		[Test]
		public void GridComponent1()
		{
			DoGet("usingcomponent2/GridComponent1.rails");

			AssertReplyContains("<table>    <th>EMail</th>\r\n    <th>Phone</th>\r\n  <tr>    <td>hammett</td>\r\n    <td>111</td>\r\n  </tr><tr>    <td>Peter Griffin</td>\r\n    <td>222</td>\r\n  </tr></table>");
		}

		[Test]
		public void GridComponent2()
		{
			DoGet("usingcomponent2/GridComponent2.rails");

			AssertReplyContains("<table>    <th>EMail</th>\r\n    <th>Phone</th>\r\n  <tr><td colspan=2>Nothing here</td>\r\n  </tr></table>");
		}

		[Test]
		public void ComponentAndParams1()
		{
			DoGet("usingcomponent2/ComponentAndParams1.rails");

			AssertReplyContains("1 2 True Something hello");
		}
		
		[Test]
		public void ChildContentComponent1()
		{
			DoGet("usingcomponent2/ChildContentComponent1.rails");
			AssertReplyContains("View content and Something");
		}
		
		[Test]
		public void ChildContentComponent2()
		{
			DoGet("usingcomponent2/ChildContentComponent2.rails");
			AssertReplyContains("View content and 1 2 True Something hello");
		}

		[Test]
		public void CanRenderMultipleDynamicComponents() 
		{
			DoGet("usingcomponent/dynamiccomponent.rails");
			AssertReplyContains("Hello from SimpleInlineViewComponent");
			AssertReplyContains("This is a view used by a component");
		}

		[Test]
		public void AutoParameterBindingMustBeAbleToPerformSimpleConversions()
		{
			DoGet("usingcomponent2/autoparameterbinding1.rails");
			AssertReplyContains("'1' 'xpto' 'something.castle?val=1'");
		}

		[Test]
		public void AutoParameterBinding_IdIsARequiredParameter()
		{
			DoGet("usingcomponent2/autoparameterbinding2.rails");
			AssertReplyContains("The parameter 'Id' is required by the ViewComponent " + 
				"AutoParameterBinding but was not passed or had a null value");
		}

		private void AssertOutput(String expected, object output)
		{
			Assert.AreEqual(NormalizeWhitespace(expected), NormalizeWhitespace(output.ToString()));
		}

		private static string NormalizeWhitespace(String s)
		{
			return s.Replace("\r\n", "\n");
		}
	}
}
